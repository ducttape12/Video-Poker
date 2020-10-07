using System;
using System.Collections.Generic;
using System.Text;

namespace VideoPoker.PayTables
{
    public class Flush : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            for(int i = 1; i < cards.Count; i++)
            {
                var current = cards[i];
                var previous = cards[i - 1];

                if(current.Suit != previous.Suit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
