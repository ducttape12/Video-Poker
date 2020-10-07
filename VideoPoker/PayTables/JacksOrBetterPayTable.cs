using System;
using System.Collections.Generic;
using System.Text;

namespace VideoPoker.PayTables
{
    public class JacksOrBetterPayTable : IPayTable
    {
        public IList<WinCombination> WinCombinations => new List<WinCombination>
        {
            new WinCombination("Royal Flush",             new RoyalFlush(),          250, 500, 750, 1000, 5000),
            new WinCombination("Straight Flush",          new StraightFlush(),        50, 100, 150,  200,  250),
            new WinCombination("4 of a Kind",             new FourOfAKind(),          25,  50,  75,  100,  125),
            new WinCombination("Full House",              new FullHouse(),             6,  12,  18,   24,   30),
            new WinCombination("Flush",                   new Flush(),                 5,  10,  15,   20,   25),
            new WinCombination("Straight",                new Straight(),              4,   8,  12,   16,   20),
            new WinCombination("3 of a Kind",             new ThreeOfAKind(),          3,   6,   9,   12,   15),
            new WinCombination("2 Pair",                  new TwoPair(),               2,   4,   6,    8,   10),
            new WinCombination("Pair of Jacks or Better", new PairOfJacksOrBetter(),   1,   2,   3,    4,    5),
        };

        public string Description => "Jacks or Better";

        public WinCombination CheckForWin(IList<Card> hand)
        {
            foreach(var winCombination in WinCombinations)
            {
                if(winCombination.WinChecker.CheckForWin(hand))
                {
                    return winCombination;
                }
            }

            return null;
        }
    }
}
