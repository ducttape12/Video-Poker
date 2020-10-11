using System;
using VideoPoker;
using VideoPoker.PayTables;
using System.Linq;
using System.Collections.Generic;

namespace VideoPokerCli
{
    public class Program
    {
        private const int MinimumWindowWidth = 80;
        private const int MinimumWindowHeight = 30;

        private static bool EnvironmentCheck()
        {
            if(Console.WindowWidth < MinimumWindowWidth || Console.WindowHeight < MinimumWindowHeight)
            {
                Console.WriteLine($"The console must be a minimum of {MinimumWindowWidth} x {MinimumWindowHeight}");
                return false;
            }

            return true;
        }

        public static void Main(string[] args)
        {
            if (!EnvironmentCheck())
            {
                return;
            }

            Console.Clear();
            Player player = PlayerSetup();
            var payTable = GetPayTable();

            PrintPayTable(payTable);

            var bet = GetMachineBetValue();
            var game = new Game(player, payTable);
            MachineDisplay.DisplayMachine(game, bet, "Play 1-5 coins or ESC for options.");
            do
            {
                var choice = GetUserInput(true, false);

                switch (choice)
                {
                    case Choice.Options:
                        Console.Clear();
                        payTable = GetPayTable();
                        bet = GetMachineBetValue();
                        game = new Game(player, payTable);
                        MachineDisplay.DisplayMachine(game, bet, "Play 1-5 coins or ESC for options.");
                        break;
                    
                    case Choice.One:
                        PlayGame(game, bet, 1);
                        break;
                    case Choice.Two:
                        PlayGame(game, bet, 2);
                        break;
                    case Choice.Three:
                        PlayGame(game, bet, 3);
                        break;
                    case Choice.Four:
                        PlayGame(game, bet, 4);
                        break;
                    case Choice.Five:
                        PlayGame(game, bet, 5);
                        break;
                }
            } while (player.Money > 0);

            Console.WriteLine("Oh no, you're penniless!  Game over.");
            Console.ReadLine();
        }

        private static Player PlayerSetup()
        {
            Console.Write("Enter your name: ");
            var name = Console.ReadLine();
            var player = new Player { Name = name, Money = 100m };
            return player;
        }

        private static void PlayGame(Game game, decimal bet, int coins)
        {
            game.InitialDeal(bet, coins);

            Choice choice;

            do
            {
                MachineDisplay.DisplayMachine(game, bet, "Hold cards 1-5 or ENTER to redeal.", coins);

                choice = GetUserInput(false, true);

                switch (choice)
                {
                    case Choice.One:
                        game.ToggleCardHold(0);
                        break;

                    case Choice.Two:
                        game.ToggleCardHold(1);
                        break;

                    case Choice.Three:
                        game.ToggleCardHold(2);
                        break;

                    case Choice.Four:
                        game.ToggleCardHold(3);
                        break;

                    case Choice.Five:
                        game.ToggleCardHold(4);
                        break;
                }

            } while (choice != Choice.Accept);

            var results = game.ReDeal();

            var message = "Game over. Play 1-5 coins.";

            if(results.GameState == GameState.Won)
            {
                message = $"Won ${results.WinAmount:0.##} with {results.WiningCombination.Description}!";
            }

            MachineDisplay.DisplayMachine(game, bet, message, coins);
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
            Options,
            Accept,
            One,
            Two,
            Three,
            Four,
            Five
        }

        private static Choice GetUserInput(bool allowOptions, bool allowAccept)
        {
            while(true)
            {
                var choice = Console.ReadKey();

                switch(choice.Key)
                {
                    case ConsoleKey.Escape:
                        if(allowOptions)
                        {
                            return Choice.Options;
                        }
                        break;
                        
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return Choice.One;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return Choice.Two;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return Choice.Three;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        return Choice.Four;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        return Choice.Five;

                    case ConsoleKey.Enter:
                        if(allowAccept)
                        {
                            return Choice.Accept;
                        }
                        break;

                    default:
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
