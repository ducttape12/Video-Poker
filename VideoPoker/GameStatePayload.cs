using VideoPoker.PayTables;

namespace VideoPoker
{
    public class GameStatePayload
    {
        public GameStatePayload(GameState gameState) : this(gameState, default, default)
        { }

        public GameStatePayload(GameState gameState, WinCombination winingCombination, decimal winAmount)
        {
            GameState = gameState;
            WiningCombination = winingCombination;
            WinAmount = winAmount;
        }

        public GameState GameState { get; set; }
        public WinCombination WiningCombination { get; set; }
        public decimal WinAmount { get; set; }
    }
}
