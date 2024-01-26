/**
*--------------------------------------------------------------------
* File name: Playlist.cs
* Project name: Playlist Manager
* Solution name: Playlist Manager
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/26/2024
* -------------------------------------------------------------------
*/

using PlaylistManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager
{
    internal class Playlist : SongList
    {
        public List<Song> TrackList { get {return this.songList; } }
        public string Name { get { return this.title; } }
        public override List<Song> AddSong(Song song)
        {

            this.songList.Add(song);

            runTime += song.songLength;

            return songList;
        }

        public override void GetSong(string song)
        {
            throw new NotImplementedException();
        }

        public override List<Song> RemoveSong(Song song)
        {
            this.songList.Remove(song);
            runTime -= song.songLength;

            return this.songList;
        }
        public void Shuffle()
        {
            List<Song> shuffled = new List<Song>();

            Random random = new Random();

            while(shuffled.Count < this.songList.Count)
            {
                int songNum = random.Next(0, this.songList.Count);

                if (!shuffled.Contains(this.songList[songNum]))
                {
                    shuffled.Add(this.songList[songNum]);
                }
            }

            this.songList = shuffled;
        }

        public override string ToString()
        {
            string output = $"{this.title}\n";

            foreach (Song song in this.songList)
            {
                output += $"\t{song.name} - {song.artist}\n";
            }

            output += $"Total Run Time: {this.runTime / 60} minutes and {this.runTime % 60} seconds";

            return output;
        }

        public Playlist(string name) : base()
        { 
            this.title = name;
        }

        public Playlist() : this(String.Empty) { }
    }
}
