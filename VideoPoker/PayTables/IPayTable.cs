using System;
using System.Collections.Generic;
using System.Text;

namespace VideoPoker.PayTables
{
    public interface IPayTable
    {
        string Description { get; }
        IList<WinCombination> WinCombinations { get; }
        WinCombination CheckForWin(IList<Card> hand);
    }
}
