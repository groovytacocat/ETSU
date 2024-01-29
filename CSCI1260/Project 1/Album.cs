/**
*--------------------------------------------------------------------
* File name: Album.cs
* Project name: Playlist Manager
* Solution name: Playlist Manager
*--------------------------------------------------------------------
* Author’s name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/26/2024
* -------------------------------------------------------------------
*/

using System;

namespace PlaylistManager
{
    public class Album : SongList
    { 
        public List<string> bandMembers { get; set; }
        public string bandName { get; set; }
        public int releaseDate { get; set; }

        public List<Song> TrackList { get { return this.songList; } }
        public string AlbumName
        {
            get { return this.title; }
            set { this.title = value; }
        }


        /// <inheritdoc />
        public override List<Song> AddSong(Song song)
        {
            this.songList.Add(song);

            this.runTime += song.songLength;

            return this.songList;
        }

        public override string GetSong(int songNum)
        {
            return this.songList[songNum].ToString();
        }

        /// <summary>
        /// Converts Album's information to a string for printing
        /// </summary>
        /// <returns><see cref="String"/> containing information on the album</returns>
        public override string ToString()
        {
            string output = $"Album Name: {this.title}\nBand: {this.bandName}\nRelease Date: {this.releaseDate}\nTrack List:\n";

            foreach(Song s in  songList)
            {
                output += $"\t{s.name}\n";
            }

            output += $"Total Runtime: {this.runTime / 60} minutes and {this.runTime % 60} seconds";

            return output;
        }

        /// <summary>
        /// Constructor for <see cref="Album"/>
        /// </summary>
        /// <param name="albumName">Name of Album as a <see cref="String"/></param>
        /// <param name="bandName">Name of band as a <see cref="String"/></param>
        /// <param name="releaseDate">Year album was released as an <see cref="int"/> </param>
        public Album(string albumName, string bandName, List<string> inMembers, int releaseDate) : base()
        {
            this.title = albumName;
            this.bandMembers = inMembers;
            this.bandName = bandName;
            this.releaseDate = releaseDate;
        }
        public Album() : this("No Album", "N/A", new List<string>(), 0) { }
    }
}
