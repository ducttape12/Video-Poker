namespace VideoPoker
{
    public class Card
    {
        public Card(FaceValue faceValue, Suit suit)
        {
            FaceValue = faceValue;
            Suit = suit;
        }

        public FaceValue FaceValue { get; }
        public Suit Suit { get; }
    }
}
