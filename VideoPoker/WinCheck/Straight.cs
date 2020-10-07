using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VideoPoker.PayTables
{
    public class Straight : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var sortedCards = cards.OrderBy(card => card.FaceValue).ToList();

            for(var i = 1; i < sortedCards.Count; i++)
            {
                var current = sortedCards[i];
                var previous = sortedCards[i - 1];

                if(previous.FaceValue + 1 != current.FaceValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
