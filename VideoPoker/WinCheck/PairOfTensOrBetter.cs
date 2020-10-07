using System.Collections.Generic;

namespace VideoPoker.PayTables
{
    public class PairOfTensOrBetter : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var tensOrBetter = new Dictionary<FaceValue, int>
            {
                {FaceValue.Ten, 0 },
                {FaceValue.Jack, 0 },
                {FaceValue.Queen, 0 },
                {FaceValue.King, 0 },
                {FaceValue.Ace, 0 }
            };

            foreach (var card in cards)
            {
                if(tensOrBetter.ContainsKey(card.FaceValue))
                {
                    tensOrBetter[card.FaceValue]++;
                }
            }

            foreach(var faceValue in tensOrBetter.Keys)
            {
                if (tensOrBetter[faceValue] > 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
