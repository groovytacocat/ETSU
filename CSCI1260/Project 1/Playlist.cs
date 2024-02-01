/**
*--------------------------------------------------------------------
* File Name: Playlist.cs
* Project Name: Playlist Manager
* Solution Name: Playlist Manager
*--------------------------------------------------------------------
* Author’s Name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/30/2024
* -------------------------------------------------------------------
*/

using MyDLL;
using PlaylistManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager
{
    /// <summary>
    /// Derived class of <see cref="SongList"/> that represents a user created Playlist of <see cref="Song"/>s from various sources
    /// </summary>
    internal class Playlist : SongList
    {
        public List<Song> TrackList { get {return this.songList; } }
        public string Name { get { return this.title; } }

        /// <inheritdoc/>
        public override List<Song> AddSong(Song song)
        {

            this.songList.Add(song);
            runTime += song.Duration;

            return songList;
        }

        /// <summary>
        /// Prints the information of a <see cref="Song"/> in the album's <see cref="List{Song}"/>
        /// </summary>
        /// <param name="songNum">Index of the <see cref="Song"/></param>
        /// <returns>Returns the <see cref="String"/> containing the <see cref="Song"/>'s information</returns>
        public override string GetSong(int songNum)
        {
            return this.songList[songNum].ToString();
        }

        /// <summary>
        /// Removes a <see cref="Song"/> from the <see cref="Playlist"/> and updates the total runtime of the <see cref="Playlist"/>
        /// </summary>
        /// <param name="song"><see cref="Song"/> to be removed</param>
        /// <returns>Returns the <see cref="List{T}"/> of <see cref="Song"/>s in the <see cref="Playlist"/>  </returns>
        public List<Song> RemoveSong(Song song)
        {
            this.songList.Remove(song);
            runTime -= song.Duration;

            return this.songList;
        }

        /// <summary>
        /// Shuffles the elements of the <see cref="List{T}"/>
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            int maxSwapIndex = this.TrackList.Count - 1;

            for(int i = maxSwapIndex; i > 1; i--)
            {
                int swapIndex = random.Next(0, i + 1);

                Song swap = this.TrackList[swapIndex];

                this.TrackList[swapIndex] = this.TrackList[i];

                this.TrackList[i] = swap;
            }
        }

        /// <summary>
        /// Formats the <see cref="Playlist"/>'s information into a readable string
        /// </summary>
        /// <returns><see cref="String"/> containing the <see cref="Playlist"/>'s information</returns>
        public override string ToString()
        {
            string output = $"Name: {this.title}\n";

            foreach (Song song in this.songList)
            {
                output += $"\t{song.Name} - {song.Artist}\n";
            }

            output += $"Total Run Time: {this.runTime / 60} minutes and {this.runTime % 60} seconds";

            return output;
        }

        /// <summary>
        /// Constructor for a <see cref="Playlist"/>
        /// </summary>
        /// <param name="name">Name of the <see cref="Playlist"/></param>
        public Playlist(string name) : base(name) { }
    }
}
