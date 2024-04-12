using System;
namespace War.Data
{
    public class PlayedCards
    {
        /// <summary>
        /// Dictionary containing <see cref="string"/> as keys representing player names and <see cref="Card"/> values
        /// representing the <see cref="Card"/> played by player that turn
        /// </summary>
        public Dictionary<string, Card> playCards { get; private set; }

        public PlayedCards()
        {
            this.playCards = new Dictionary<string, Card>();
        }

        /// <summary>
        /// Method to add player to dictionary if not already present, or otherwise update the value 
        /// to be the <see cref="Card"/> played for a particular round
        /// </summary>
        /// <param name="player"><see cref="string"/> representing player name</param>
        /// <param name="playerCard"><see cref="Card"/> being played that round</param>
        public void GetCards(string player, Card playerCard)
        {
            playCards[player] = playerCard;
        }
    }
}

