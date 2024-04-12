using System;
namespace War.Data
{
    public class PlayerHands
    {
        /// <summary>
        /// Dictionary containing <see cref="string"/> keys that are player names and <see cref="Hand"/> values representing that players <see cref="Hand"/>
        /// </summary>
        public Dictionary<string, Hand> playHand { get; private set; }

        public PlayerHands()
        {
            this.playHand = new Dictionary<string, Hand>();
        }

        /// <summary>
        /// Adds a <see cref="string"/> representing a player's name to dictionary with an empty hand
        /// </summary>
        /// <param name="playerName"><see cref="string"/> name of player to be added</param>
        public void AddPlayer(string playerName)
        {
            playHand.Add(playerName, new Hand());
        }

        /// <summary>
        /// Removes a player from the dictionary
        /// </summary>
        /// <param name="playerName"><see cref="string"/> name of player to be removed</param>
        public void RemovePlayer(string playerName)
        {
            playHand.Remove(playerName);
        }
    }
}