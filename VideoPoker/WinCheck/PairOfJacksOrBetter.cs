using System.Collections.Generic;

namespace VideoPoker.PayTables
{
    public class PairOfJacksOrBetter : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var jacksOrBetter = new Dictionary<FaceValue, int>
            {
                {FaceValue.Jack, 0 },
                {FaceValue.Queen, 0 },
                {FaceValue.King, 0 },
                {FaceValue.Ace, 0 }
            };

            foreach (var card in cards)
            {
                if(jacksOrBetter.ContainsKey(card.FaceValue))
                {
                    jacksOrBetter[card.FaceValue]++;
                }
            }

            foreach(var faceValue in jacksOrBetter.Keys)
            {
                if (jacksOrBetter[faceValue] > 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
