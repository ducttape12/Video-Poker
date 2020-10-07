using System.Collections.Generic;

namespace VideoPoker.PayTables
{
    public interface IWinChecker
    {
        bool CheckForWin(IList<Card> hand);
    }
}
