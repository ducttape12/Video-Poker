using System;
using System.Collections.Generic;

namespace VideoPoker
{
    public class Deck
    {
        private readonly Stack<Card> cards;

        public Deck()
        {
            var cards = BuildCards();
            FisherYatesShuffle(cards);

            this.cards = new Stack<Card>(cards);
        }

        private List<Card> BuildCards()
        {
            var faceValues = Enum.GetValues(typeof(FaceValue));
            var suits = Enum.GetValues(typeof(Suit));
            var cards = new List<Card>(faceValues.Length * suits.Length);

            foreach (FaceValue faceValue in faceValues)
            {
                foreach (Suit suit in suits)
                {
                    cards.Add(new Card(faceValue, suit));
                }
            }

            return cards;
        }

        private static void FisherYatesShuffle<T>(IList<T> collection)
        {
            var random = new Random();

            for(var i = collection.Count - 1; i > 0; i--)
            {
                var currentValue = collection[i];
                var itemIndexToSwap = random.Next(0, i);

                collection[i] = collection[itemIndexToSwap];
                collection[itemIndexToSwap] = currentValue;
            }
        }

        public int CardsRemaining => cards.Count;

        public Card Draw()
        {
            return cards.Pop();
        }
    }
}
