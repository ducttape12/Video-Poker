using System;
using System.Collections.Generic;
using System.Text;

namespace VideoPoker.PayTables
{
    public class FullHouse : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var fullHouse = new Dictionary<FaceValue, int>();

            foreach (var card in cards)
            {
                if (fullHouse.ContainsKey(card.FaceValue))
                {
                    fullHouse[card.FaceValue]++;
                }
                else
                {
                    fullHouse.Add(card.FaceValue, 1);
                }
            }

            bool threeMatched = false;
            bool twoMatched = false;
            foreach (var faceValue in fullHouse.Keys)
            {
                threeMatched = threeMatched || fullHouse[faceValue] == 3;
                twoMatched = twoMatched || fullHouse[faceValue] == 2;
            }

            return twoMatched && threeMatched;
        }
    }
}
