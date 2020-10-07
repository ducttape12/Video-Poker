using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VideoPoker.PayTables
{
    public class FourOfAKind : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var fourOfAKind = new Dictionary<FaceValue, int>();

            foreach (var card in cards)
            {
                if (fourOfAKind.ContainsKey(card.FaceValue))
                {
                    fourOfAKind[card.FaceValue]++;
                }
                else
                {
                    fourOfAKind.Add(card.FaceValue, 1);
                }
            }

            return fourOfAKind.Keys.Any(faceValue => fourOfAKind[faceValue] == 4);
        }
    }
}
