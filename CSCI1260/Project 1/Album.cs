/**
*--------------------------------------------------------------------
* File Name: Album.cs
* Project Name: Playlist Manager
* Solution Name: Playlist Manager
*--------------------------------------------------------------------
* Author’s Name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/30/2024
* -------------------------------------------------------------------
*/

using System;

namespace PlaylistManager
{
    /// <summary>
    /// Represents a type of <see cref="SongList"/> that has a collection of <see cref="Song"/>s 
    /// by the same artist.
    /// </summary>
    public class Album : SongList
    { 
        public List<string> BandMembers { get; private set; }
        public string BandName { get; private set; }
        public int ReleaseDate { get; private set; }
        public List<Song> TrackList { get; private set;  }
        public string AlbumName { get; private set; }


        /// <inheritdoc />
        public override List<Song> AddSong(Song song)
        {
            this.songList.Add(song);

            this.runTime += song.Duration;

            return this.songList;
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
        /// Converts Album's information to a string for printing
        /// </summary>
        /// <returns><see cref="String"/> containing information on the album</returns>
        public override string ToString()
        {
            string output = $"Album Name: {this.title}\nBand: {this.BandName}\nRelease Date: {this.ReleaseDate}\nTrack List:\n";

            foreach(Song s in  songList)
            {
                output += $"\t{s.Name}\n";
            }

            output += $"Total Runtime: {this.runTime / 60} minutes and {this.runTime % 60} seconds";

            return output;
        }

        /// <summary>
        /// Constructor for <see cref="Album"/>
        /// </summary>
        /// <param Name="albumName">Name of Album as a <see cref="String"/></param>
        /// <param Name="BandName">Name of band as a <see cref="String"/></param>
        /// <param Name="ReleaseDate">Year album was released as an <see cref="int"/> </param>
        public Album(string albumName, string bandName, List<string> inMembers, int releaseDate) : base(albumName)
        {
            this.BandMembers = inMembers;
            this.BandName = bandName;
            this.ReleaseDate = releaseDate;
        }
        public Album() : this("No Album", "N/A", new List<string>(), 0) { }
    }
}
