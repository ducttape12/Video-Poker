using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoPoker.PayTables
{
    public class RoyalFlush : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var sortedCards = cards.OrderBy(card => card.FaceValue).ToList();

            if(sortedCards.Last().FaceValue != FaceValue.Ace)
            {
                return false;
            }

            for (var i = 1; i < sortedCards.Count; i++)
            {
                var current = sortedCards[i];
                var previous = sortedCards[i - 1];

                if (previous.FaceValue + 1 != current.FaceValue ||
                    previous.Suit != current.Suit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
