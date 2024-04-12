using System;
namespace War.Data
{
    public class Deck
    {
        public Stack<Card> cards { get; private set; }

        /// <summary>
        /// Constructor that creates a random deck of 52 <see cref="Card"/>s
        /// by generating a random number between 0 and 12 representing a <see cref="Card.Rank"/> and 0-3 representing a <see cref="CardSuit"/>.
        /// Allows for duplicate <see cref="Card"/> values unlike a standard deck
        /// </summary>
        public Deck()
        { 
            this.cards = new Stack<Card>();

            Random rand = new Random();
            int suitRand = 0;
            int rankRand = 0;

            for (int i = 0; i < 52; i++)
            {
                suitRand = rand.Next(0, 4);
                rankRand = rand.Next(0, 13);

                Data.Card randCard = new Card((CardSuit)suitRand, rankRand);

                this.AddCard(randCard);
            }
        }

        /// <summary>
        /// Property to return the Count of the <see cref="Stack{T}"/>
        /// </summary>
        public int Count
        {
            get
            {
                return this.cards.Count();
            }
        }

        /// <summary>
        /// Method to add <see cref="Card"/>s to the <see cref="Deck"/>
        /// </summary>
        /// <param name="inCard"><see cref="Card"/> to be added</param>
        public void AddCard(Card inCard)
        {
            this.cards.Push(inCard);
        }

        /// <summary>
        /// Method to remove a <see cref="Card"/> from the <see cref="Deck"/>
        /// </summary>
        /// <returns><see cref="Card"/> that was returned from <see cref="Stack{T}.Pop"/></returns>
        public Card DealCard()
        {
            return this.cards.Pop();
        }
    }
}

