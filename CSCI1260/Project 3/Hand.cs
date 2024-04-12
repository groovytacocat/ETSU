using System;
namespace War.Data
{
    public class Hand
    {
        /// <summary>
        /// <see cref="Queue{T}"/> of <see cref="Card"/> that represent a players <see cref="Hand"/>
        /// </summary>
        public Queue<Card> cards { get; private set; }

        /// <summary>
        /// Method to remove a <see cref="Card"/> from a <see cref="Hand"/>
        /// </summary>
        /// <returns><see cref="Card"/> from the <see cref="Queue{T}"/></returns>
        public Card TakeCard()
        {
            return cards.Dequeue();
        }

        /// <summary>
        /// Method to add <see cref="Card"/>(s) to a given <see cref="Hand"/>
        /// </summary>
        /// <param name="input"></param>
        public void AddCard(Card input)
        {
            cards.Enqueue(input);
        }

        public Hand()
        {
            this.cards = new Queue<Card>();
        }
    }
}

