using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VideoPoker;
using VideoPoker.PayTables;

namespace VideoPokerCli
{
    public static class MachineDisplay
    {
        private const int machineWidthNeeded = 67;
        private const int payTableDisplayWidth = 57;
        private const int combinationDisplayWidth = 26;
        private const int payoutDisplayWidth = 4;
        private const int coinDisplayWidth = 7;
        private const int messageDisplayWidth = 41;
        private const int betDisplayWidth = 5;

        public static void DisplayMachine(Game game, decimal bet, string message, int? coins = null)
        {
            var renderedCards = game.GetDrawnCards().Select(c => RenderCard(c)).ToList();
            var coinDisplay = coins.HasValue ? $"{coins.Value} coin{(coins.Value > 1 ? "s" : string.Empty)}" : string.Empty;
            var betDisplay = $"${bet:0.##}";
            var playerDisplay = $"{game.Player.Name}: ${game.Player.Money:0.00}";

            Console.Clear();
            DisplayLine();
            DisplayLine($"    ╔═════════════════════════════════════════════════════════╗");
            DisplayLine($"    ║{AlignText.AlignAndFit(game.PayTable.Description, Alignment.Center, payTableDisplayWidth)}║");
            DisplayLine($"╔═══╩════════════════════════╤══════╤══════╤══════╤══════╤════╩═╤═╗");
            DisplayLine($"║ Combination                │    1 │    2 │    3 │    4 │    5 │ ║");
            DisplayLine($"╟────────────────────────────┼──────┼──────┼──────┼──────┼──────┼─╢");

            foreach(var winCombo in game.PayTable.WinCombinations)
            {
                DisplayLine($"║ {AlignText.AlignAndFit(winCombo.Description, Alignment.Left, combinationDisplayWidth)} │" +
                    $" {AlignText.AlignAndFit(winCombo.OneCreditPayout, Alignment.Right, payoutDisplayWidth)} │" +
                    $" {AlignText.AlignAndFit(winCombo.TwoCreditPayout, Alignment.Right, payoutDisplayWidth)} │" +
                    $" {AlignText.AlignAndFit(winCombo.ThreeCreditPayout, Alignment.Right, payoutDisplayWidth)} │" +
                    $" {AlignText.AlignAndFit(winCombo.FourCreditPayout, Alignment.Right, payoutDisplayWidth)} │" +
                    $" {AlignText.AlignAndFit(winCombo.FiveCreditPayout, Alignment.Right, payoutDisplayWidth)} │ ║");
            }

            DisplayLine($"╟────────────────────────────┴──────┴──────┴──────┴──────┴──────┴─╢");
            DisplayLine($"║                                                                 ║");
            DisplayLine($"║   {renderedCards[0][0]}      {renderedCards[1][0]}      {renderedCards[2][0]}      {renderedCards[3][0]}      {renderedCards[4][0]}   ║");
            DisplayLine($"║   {renderedCards[0][1]}      {renderedCards[1][1]}      {renderedCards[2][1]}      {renderedCards[3][1]}      {renderedCards[4][1]}   ║");
            DisplayLine($"║   {renderedCards[0][2]}      {renderedCards[1][2]}      {renderedCards[2][2]}      {renderedCards[3][2]}      {renderedCards[4][2]}   ║");
            DisplayLine($"║   {renderedCards[0][3]}      {renderedCards[1][3]}      {renderedCards[2][3]}      {renderedCards[3][3]}      {renderedCards[4][3]}   ║");
            DisplayLine($"║   {renderedCards[0][4]}      {renderedCards[1][4]}      {renderedCards[2][4]}      {renderedCards[3][4]}      {renderedCards[4][4]}   ║");
            DisplayLine($"║      1            2            3            4            5      ║");
            DisplayLine($"╟─────────────────────────────────────────────────────────────────╢");
            DisplayLine($"║ ┌─────────╥───────────────────────────────────────────╥───────┐ ║");
            DisplayLine($"║ │ {AlignText.AlignAndFit(coinDisplay, Alignment.Center, coinDisplayWidth)} ║ {AlignText.AlignAndFit(message, Alignment.Center, messageDisplayWidth)} ║ {AlignText.AlignAndFit(betDisplay, Alignment.Right, betDisplayWidth)} │ ║");
            DisplayLine($"║ └─────────╨───────────────────────────────────────────╨───────┘ ║");
            DisplayLine($"╚═════════════════════════════════════════════════════════════════╝");
            DisplayLine();
            DisplayLine($"{AlignText.AlignAndFit(playerDisplay, Alignment.Center, machineWidthNeeded)}");
        }

        private static void DisplayLine()
        {
            Console.WriteLine();
        }

        private static void DisplayLine(string line)
        {
            var display = AlignText.AlignAndFit(line, Alignment.Center, Console.WindowWidth);
            Console.WriteLine(display);
        }

        private static IList<string> RenderCard(DrawnCard card)
        {
            if(card == null)
            {
                return RenderNoCard();
            }
            else if(card.OnHold)
            {
                return RenderOnHoldCard(card.Card);
            }
            else
            {
                return RenderShownCard(card.Card);
            }
        }

        private static IList<string> RenderNoCard()
        {
            var display = new List<string>
            {
                "▄▄▄▄▄▄▄",
                "███████",
                "███████",
                "███████",
                "▀▀▀▀▀▀▀"
            };

            return display;
        }

        private static IList<string> RenderOnHoldCard(Card card)
        {
            var display = RenderShownCard(card);

            display[3] = "│ HLD │";

            return display;
        }

        private static IList<string> RenderShownCard(Card card)
        {
            var value = card.FaceValue switch
            {
                FaceValue.Two => "2",
                FaceValue.Three => "3",
                FaceValue.Four => "4",
                FaceValue.Five => "5",
                FaceValue.Six => "6",
                FaceValue.Seven => "7",
                FaceValue.Eight => "8",
                FaceValue.Nine => "9",
                FaceValue.Ten => "10",
                FaceValue.Jack => "J",
                FaceValue.Queen => "Q",
                FaceValue.King => "K",
                FaceValue.Ace => "A",
                _ => string.Empty
            };

            var suit = card.Suit switch
            {
                Suit.Clubs => "♣",
                Suit.Diamonds => "♦",
                Suit.Hearts => "♥",
                Suit.Spades => "♠",
                _ => string.Empty
            };

            var display = new List<string>
            {
                "┌─────┐",
                "│     │",
                $"│ {AlignText.AlignAndFit(value, Alignment.Left, 2)}{suit} │",
                "│     │",
                "└─────┘"
            };

            return display;
        }
    }
}
