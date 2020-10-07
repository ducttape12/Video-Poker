namespace VideoPoker
{
    public class DrawnCard
    {
        public DrawnCard(Card card)
        {
            Card = card;
        }
        public Card Card { get; set; }
        public bool OnHold { get; set; }
    }
}
