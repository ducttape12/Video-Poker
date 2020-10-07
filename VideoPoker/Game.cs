using System.Collections.Generic;
using VideoPoker.PayTables;
using System.Linq;

namespace VideoPoker
{
    public class Game
    {
        private readonly Player player;
        private decimal bet;
        private int coins;
        private readonly IPayTable payTable;
        private Deck deck;
        private IList<DrawnCard> hand = new List<DrawnCard>();

        private GameState gameState;

        public const int CardsToPlay = 5;

        public Game(Player player, IPayTable payTable)
        {
            deck = new Deck();
            this.player = player;
            this.payTable = payTable;
        }

        public bool InitialDeal(decimal bet, int coins)
        {
            var totalBet = bet * coins;

            if (player.Money < totalBet)
            {
                return false;
            }

            this.bet = bet;
            this.coins = coins;

            InitialDraw();
            player.Money -= totalBet;

            gameState = GameState.FirstDeal;
            return true;
        }

        public GameStatePayload ReDeal()
        {
            if (gameState != GameState.FirstDeal)
            {
                return new GameStatePayload(gameState);
            }

            ReplaceCards();

            var winningCombination = payTable.CheckForWin(hand.Select(c => c.Card).ToList());

            if(winningCombination == null)
            {
                gameState = GameState.Lost;
                return new GameStatePayload(gameState);
            }

            gameState = GameState.Won;
            var winAmount = winningCombination.GetPayoutMultiplier(coins) * bet;
            player.Money += winAmount;
            return new GameStatePayload(gameState, winningCombination, winAmount);
        }

        private void ReplaceCards()
        {
            for (var i = 0; i < hand.Count; i++)
            {
                var drawnCard = GetDrawnCard(i);

                if (!drawnCard.OnHold)
                {
                    var newDraw = new DrawnCard(deck.Draw());
                    hand[i] = newDraw;
                }
            }
        }

        private void InitialDraw()
        {
            for (var i = 0; i < CardsToPlay; i++)
            {
                var drawnCard = new DrawnCard(deck.Draw());
                hand.Add(drawnCard);
            }
        }

        public DrawnCard GetDrawnCard(int index)
        {
            if(index < 0 || index > hand.Count)
            {
                return null;
            }

            return hand[index];
        }

        public bool ToggleCardHold(int index)
        {
            if (gameState != GameState.FirstDeal || index < 0 || index > hand.Count)
            {
                return false;
            }

            hand[index].OnHold = !hand[index].OnHold;
            return true;
        }
    }
}
