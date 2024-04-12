using System;
namespace War.Data
{
    public class Card
    {
        /// <summary>
        /// Represents the <see cref="Card"/>'s suit
        /// </summary>
        public CardSuit Suit { get; private set; }

        /// <summary>
        /// <see cref="int"/> representing <see cref="Card"/> value for comparison
        /// </summary>
        public int Rank { get; private set; }

        public Card(CardSuit inSuit, int inRank)
        {
            this.Suit = inSuit;
            this.Rank = inRank;
        }
    }
}

