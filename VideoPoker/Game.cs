using System.Collections.Generic;
using VideoPoker.PayTables;
using System.Linq;

namespace VideoPoker
{
    public class Game
    {
        private decimal bet;
        private int coins;
        private Deck deck;
        private readonly IList<DrawnCard> hand = new List<DrawnCard> { null, null, null, null, null };

        private GameState gameState;

        public const int CardsToPlay = 5;

        public IPayTable PayTable { get; }
        public Player Player { get; }

        public Game(Player player, IPayTable payTable)
        {
            deck = new Deck();
            this.Player = player;
            this.PayTable = payTable;
        }

        public bool InitialDeal(decimal bet, int coins)
        {
            var totalBet = bet * coins;

            if (Player.Money < totalBet)
            {
                return false;
            }

            this.bet = bet;
            this.coins = coins;

            deck = new Deck();
            InitialDraw();
            Player.Money -= totalBet;

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

            var winningCombination = PayTable.CheckForWin(hand.Select(c => c.Card).ToList());

            if(winningCombination == null)
            {
                gameState = GameState.Lost;
                return new GameStatePayload(gameState);
            }

            gameState = GameState.Won;
            var winAmount = winningCombination.GetPayoutMultiplier(coins) * bet;
            Player.Money += winAmount;
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
            hand.Clear();
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
        
        public IList<DrawnCard> GetDrawnCards()
        {
            return hand;
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
