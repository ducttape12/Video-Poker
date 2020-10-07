using System.Collections.Generic;

namespace VideoPoker.PayTables
{
    public class TwoPair : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var twoPair = new Dictionary<FaceValue, int>();

            foreach (var card in cards)
            {
                if (twoPair.ContainsKey(card.FaceValue))
                {
                    twoPair[card.FaceValue]++;
                }
                else
                {
                    twoPair.Add(card.FaceValue, 1);
                }
            }

            var numberOfPairs = 0;
            foreach(var faceValue in twoPair.Keys)
            {
                if(twoPair[faceValue] == 2)
                {
                    numberOfPairs++;
                }
            }

            return numberOfPairs == 2;
        }
    }
}
