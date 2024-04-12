using System;
using War.Data;
using War.Pages;
using War.Shared;

namespace War.Services
{
    public class WarService : IWarService
    {
        /// <summary>
        /// <see cref="Hand"/> containing <see cref="Card"/>s currently in play
        /// </summary>
        public Data.Hand currentHand { get; set; } = new Data.Hand();

        /// <summary>
        /// <see cref="Deck"/> object representing a deck of cards
        /// </summary>
        public Data.Deck GameDeck { get; set; } = new Data.Deck();

        /// <summary>
        /// <see cref="PlayerHands"/> object representing each player and all of their <see cref="Card"/>s
        /// </summary>
        public Data.PlayerHands players { get; set; } = new Data.PlayerHands();

        /// <summary>
        /// <see cref="PlayedCards"/> object representing the <see cref="Card"/> played by each player for a given round
        /// </summary>
        public Data.PlayedCards playedCards { get; set; } = new Data.PlayedCards();

        /// <summary>
        /// <see cref="int"/> property to set number of players when dealing cards
        /// </summary>
        public int numPlayers { get; set; }

        /// <summary>
        /// <see cref="Dictionary{int, string}"/> that maps <see cref="int"/> value of a <see cref="Card"/> to a <see cref="string "/> representing Rank
        /// </summary>
        public Dictionary<int, string> cardRanks { get; set; } = new Dictionary<int, string>()
        {
            {0, "2"},
            {1, "3"},
            {2, "4"},
            {3, "5"},
            {4, "6"},
            {5, "7"},
            {6, "8"},
            {7, "9"},
            {8, "10"},
            {9, "J"},
            {10, "Q"},
            {11, "K"},
            {12, "A"}
        };

        /// <summary>
        /// Calculates the <see cref="int"/> number of <see cref="Card"/>s to be dealt to players evenly. (ie. 52 cards - the remainder of 52 modulo cards per player)
        /// Uses while loop to sequentially deal a <see cref="Card"/> to properly emulate real-world dealing.
        /// </summary>
        public void Deal(int numPlayers)
        {
            //Get the number of cards to be evenly dealt.
            int numCards = GameDeck.Count - (GameDeck.Count % (GameDeck.Count / numPlayers));

            //Deal all cards to players, sequentially 
            while(numCards > 0)
            {
                foreach(KeyValuePair<string, Hand> pHands in players.playHand)
                {
                    pHands.Value.AddCard(GameDeck.DealCard());
                    numCards--;
                }
            }
        }

        /// <summary>
        /// Plays a round of War.
        /// Checks and sees which players still have cards in hand and those with 0 are removed from eligible players.
        /// If only 1 player remains in eligible player pool they are declared a winner.
        /// Otherwise draws a <see cref="Card"/> from each player's <see cref="Hand"/> and places it on table.
        /// Adds <see cref="Card"/> to a <see cref="Hand"/> that holds all <see cref="Card"/>s that are in play (for cases of a tie between players)
        /// Calls <see cref="CheckRoundWin(List{string})"/> to determine a winner.
        /// </summary>
        /// <param name="inPlay">List of player names represented by a <see cref="string"/> value</param>
        /// <returns>Formatted <see cref="string"/> to notify user(s) of the winner</returns>
        public string PlayRound(List<string> inPlay)
        {
            foreach (string user in inPlay)
            {
                playedCards.GetCards(user, players.playHand[user].TakeCard());
                currentHand.AddCard(playedCards.playCards[user]);
            }

            return CheckRoundWin(inPlay);
        }

        /// <summary>
        /// Checks to see which players still have <see cref="Card"/>s in their <see cref="Hand"/>s
        /// If no cards are present they are removed from play
        /// </summary>
        /// <param name="inPlay"></param>
        public void CheckPlayers(List<string> inPlay)
        {
            for (int i = 0; i < inPlay.Count; i++)
            {
                if (players.playHand[inPlay[i]].cards.Count == 0)
                {
                    players.playHand.Remove(inPlay[i]);
                    playedCards.playCards.Remove(inPlay[i]);
                    inPlay.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Checks the Cards on the "table" to determine the winner.
        /// Adds the player with the highest card to a List that holds player(s) with highest card.
        /// If a new high card value is found list is cleared and updated to have new player
        /// If only 1 player has highest card they win the round and add all currently played cards to their hand.
        /// If there are 2+ players in the winner List, a follow-up round is played to break the tie.
        /// </summary>
        /// <param name="inPlay">List of player names represented by a <see cref="string"/> value</param>
        /// <returns>Formatted <see cref="string"/> to notify user(s) of the winner</returns>
        public string CheckRoundWin(List<string> inPlay)
        {
            List<string> winner = new List<string>();
            int highCard = -1;

            foreach(string user in inPlay)
            {
                if (playedCards.playCards[user].Rank > highCard)
                {
                    winner.Clear();
                    highCard = playedCards.playCards[user].Rank;
                    winner.Add(user);
                }
                else if(playedCards.playCards[user].Rank == highCard)
                {
                    winner.Add(user);
                }
            }

            if (winner.Count == 1)
            {
                while(currentHand.cards.Count > 0)
                {
                    players.playHand[winner[0]].AddCard(currentHand.TakeCard());
                }

                CheckPlayers(inPlay);

                if (inPlay.Count == 1)
                {
                    return $"{inPlay[0]} wins the game!";
                }
            }
            else
            {
                CheckPlayers(winner);
                if(winner.Count == 1)
                {
                    return $"{winner[0]} wins the round";
                }

                PlayRound(winner);
            }

            return $"{winner[0]} wins the round";
        }

        /// <summary>
        /// Show <see cref="Card"/> rank as a <see cref="string"/>
        /// Helper function to make the code slightly more succinct
        /// </summary>
        /// <param name="card"></param>
        /// <returns><see cref="string"/> representing <see cref="Card"/> rank based on mapped value</returns>
        public string ShowCard(Card card)
        {
            return cardRanks[card.Rank];
        }
    }
}

