using System.Collections.Generic;
using System.Linq;

namespace VideoPoker.PayTables
{
    public class ThreeOfAKind : IWinChecker
    {
        public bool CheckForWin(IList<Card> cards)
        {
            var threeOfAKind = new Dictionary<FaceValue, int>();

            foreach (var card in cards)
            {
                if (threeOfAKind.ContainsKey(card.FaceValue))
                {
                    threeOfAKind[card.FaceValue]++;
                }
                else
                {
                    threeOfAKind.Add(card.FaceValue, 1);
                }
            }

            return threeOfAKind.Keys.Any(faceValue => threeOfAKind[faceValue] == 3);
        }
    }
}
