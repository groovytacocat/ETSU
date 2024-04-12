using System;
using War.Data;
using War.Pages;
using War.Shared;

namespace War.Services
{
    public interface IWarService
    {
        public Data.Deck GameDeck { get; set; }
        public Dictionary<int, string> cardRanks { get; set; }
        public Data.PlayerHands players { get; set; }
        public Data.PlayedCards playedCards { get; set; }
        public int numPlayers { get; set; }

        public string ShowCard(Card card);
        public string PlayRound(List<string> inPlay);
        public string CheckRoundWin(List<string> inPlay);
        public void Deal(int numPlayers);
    }
}

