using System;

namespace VideoPoker.PayTables
{
    public class WinCombination
    {
        public WinCombination(string description, IWinChecker winChecker, int oneCreditPayout,
            int twoCreditPayout, int threeCreditPayout, int fourCreditPayout, int fiveCreditPayout)
        {
            Description = description;
            WinChecker = winChecker;
            OneCreditPayout = oneCreditPayout;
            TwoCreditPayout = twoCreditPayout;
            ThreeCreditPayout = threeCreditPayout;
            FourCreditPayout = fourCreditPayout;
            FiveCreditPayout = fiveCreditPayout;
        }

        public string Description { get; }
        public IWinChecker WinChecker { get; }
        public int OneCreditPayout { get; }
        public int TwoCreditPayout { get; }
        public int ThreeCreditPayout { get; }
        public int FourCreditPayout { get; }
        public int FiveCreditPayout { get; }

        public int GetPayoutMultiplier(int coins)
        {
            switch(coins)
            {
                case 1:
                    return OneCreditPayout;
                case 2:
                    return TwoCreditPayout;
                case 3:
                    return ThreeCreditPayout;
                case 4:
                    return FourCreditPayout;
                case 5:
                    return FiveCreditPayout;
                default:
                    throw new NotImplementedException("Unknown coin amount " + coins);
            }
        }
    }
}
