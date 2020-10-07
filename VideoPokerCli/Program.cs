using System;
using VideoPoker;
using VideoPoker.PayTables;
using System.Linq;
using System.Collections.Generic;

namespace VideoPokerCli
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();

            Console.WriteLine($"Welcome {name} to Video Poker!  We're going to give you $100 to get you started.  Enjoy!");

            var player = new Player { Name = name, Money = 100 };

            var payTable = GetPayTable();

            PrintPayTable(payTable);

            var bet = GetMachineBetValue();
            do
            {
                Console.WriteLine();
                Console.WriteLine($"{player.Name} has ${player.Money:0.00}");
                Console.WriteLine($"Playing ${bet:0.00} {payTable.Description}");

                var userChoice = GetUserChoice();

                switch (userChoice)
                {
                    case Choice.ChangeBet:
                        bet = GetMachineBetValue();
                        break;
                    case Choice.ChangeMachine:
                        payTable = GetPayTable();
                        break;
                    case Choice.DisplayPayTable:
                        PrintPayTable(payTable);
                        break;
                    case Choice.PlayOneCoin:
                        PlayGame(player, payTable, bet, 1);
                        break;
                    case Choice.PlayTwoCoins:
                        PlayGame(player, payTable, bet, 2);
                        break;
                    case Choice.PlayThreeCoins:
                        PlayGame(player, payTable, bet, 3);
                        break;
                    case Choice.PlayFourCoins:
                        PlayGame(player, payTable, bet, 4);
                        break;
                    case Choice.PlayFiveCoins:
                        PlayGame(player, payTable, bet, 5);
                        break;
                }
            } while (player.Money > 0);

            Console.WriteLine("Oops, you're broke.  Game over!");
            Console.ReadLine();
        }

        private static void PlayGame(Player player, IPayTable payTable, decimal bet, int coins)
        {
            var videoPokerGame = new Game(player, payTable);
            videoPokerGame.InitialDeal(bet, coins);

            Console.WriteLine();
            Console.WriteLine("First Deal:");
            ShowCards(videoPokerGame);

            Console.Write("Enter the cards you wish to hold separated by a space: ");
            var cardsToHold = Console.ReadLine();

            var cardIndexes = string.IsNullOrWhiteSpace(cardsToHold) ?
                new List<int>() :
                cardsToHold.Trim().Split(" ").Select(c => int.Parse(c)).ToList();

            foreach (var index in cardIndexes)
            {
                videoPokerGame.ToggleCardHold(index - 1);
            }

            var results = videoPokerGame.ReDeal();

            Console.WriteLine();
            Console.WriteLine("Redrawn cards:");
            ShowCards(videoPokerGame);

            Console.WriteLine();
            if (results.GameState == GameState.Lost)
            {
                Console.WriteLine($"I'm sorry, you lost ${(bet * coins):0.00}");
            }
            else
            {
                Console.WriteLine($"You won ${results.WinAmount:0.00} with a {results.WiningCombination.Description}!");
            }
        }

        private static IPayTable GetPayTable()
        {
            Console.WriteLine();
            Console.WriteLine("Machines:");
            Console.WriteLine("1. Jacks or Better");
            Console.WriteLine("2. Tens or Better");

            while(true)
            {
                Console.Write("Enter a machine choice: ");
                var machineSelection = Console.ReadLine();

                switch(machineSelection)
                {
                    case "1":
                        return new JacksOrBetterPayTable();
                    case "2":
                        return new TensOrBetterPayTable();
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }
        }

        private static void ShowCards(Game videoPokerGame)
        {
            for (var i = 0; i < Game.CardsToPlay; i++)
            {
                var drawnCard = videoPokerGame.GetDrawnCard(i);

                Console.WriteLine($"{i + 1}. {drawnCard.Card.FaceValue} of {drawnCard.Card.Suit}");
            }
        }

        private static decimal GetMachineBetValue()
        {
            Console.WriteLine();
            Console.WriteLine("Bet Levels:");
            Console.WriteLine("1. $0.25");
            Console.WriteLine("2. $0.50");
            Console.WriteLine("3. $1.00");
            Console.WriteLine("4. $5.00");
            Console.WriteLine("5. $10.00");
            Console.WriteLine("6. $50.00");
            Console.WriteLine("7. $100.00");

            var value = 0m;

            while(value == 0)
            {
                Console.Write("Enter your bet level: ");
                var input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                        value = 0.25m;
                        break;
                    case "2":
                        value = 0.50m;
                        break;
                    case "3":
                        value = 1m;
                        break;
                    case "4":
                        value = 5m;
                        break;
                    case "5":
                        value = 10m;
                        break;
                    case "6":
                        value = 50m;
                        break;
                    case "7":
                        value = 100m;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }

            return value;
        }

        private enum Choice
        {
            ChangeMachine,
            ChangeBet,
            DisplayPayTable,
            PlayOneCoin,
            PlayTwoCoins,
            PlayThreeCoins,
            PlayFourCoins,
            PlayFiveCoins
        }

        private static Choice GetUserChoice()
        {
            while(true)
            {
                Console.WriteLine("  p. Display pay table");
                Console.WriteLine("  m. Change machine");
                Console.WriteLine("  b. Change bet");
                Console.WriteLine("1-5. Number of coins to play on current machine");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch(choice.ToLowerInvariant())
                {
                    case "p":
                        return Choice.DisplayPayTable;
                    case "b":
                        return Choice.ChangeBet;
                    case "m":
                        return Choice.ChangeMachine;
                    case "1":
                        return Choice.PlayOneCoin;
                    case "2":
                        return Choice.PlayTwoCoins;
                    case "3":
                        return Choice.PlayThreeCoins;
                    case "4":
                        return Choice.PlayFourCoins;
                    case "5":
                        return Choice.PlayFiveCoins;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }
        }

        private static void PrintPayTable(IPayTable payTable)
        {
            const int descriptionPadding = 25;
            const int payoutPadding = 4;

            var payoutList = payTable
                .WinCombinations
                .Select(c => $"{c.Description,descriptionPadding} | {c.OneCreditPayout,payoutPadding} | {c.TwoCreditPayout,payoutPadding} | {c.ThreeCreditPayout,payoutPadding} | {c.FourCreditPayout,payoutPadding} | {c.FiveCreditPayout,payoutPadding} |")
                .ToList();

            var maxLineLength = payoutList.Max(p => p.Length);

            Console.WriteLine();
            Console.WriteLine(payTable.Description);
            Console.WriteLine($"{"".PadLeft(payTable.Description.Length, '=')}");
            Console.WriteLine($"{"Combination",descriptionPadding} | {"1",payoutPadding} | {"2",payoutPadding} | {"3",payoutPadding} | {"4",payoutPadding} | {"5",payoutPadding} |");
            Console.WriteLine($"{"".PadLeft(maxLineLength, '-')}");
            foreach (var payout in payoutList)
            {
                Console.WriteLine(payout);
            }
        }
    }
}
