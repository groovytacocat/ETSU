/**
*--------------------------------------------------------------------
* File Name: Song.cs
* Project Name: Playlist Manager
* Solution Name: Playlist Manager
*--------------------------------------------------------------------
* Author’s Name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/30/2024
* -------------------------------------------------------------------
*/

namespace PlaylistManager
{
    /// <summary>
    /// An Object that represents a Song
    /// </summary>
    public class Song
    {
        /// <summary>
        /// <see cref="String"/> representation of song Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="int"/> representing length of song in seconds
        /// </summary>
        public int Duration { get; private set; }

        /// <summary>
        /// <see cref="String"/> representation of Artist Name
        /// </summary>
        public string Artist { get; private set; }

        /// <summary>
        /// <see cref="Album"/> the <see cref="Song"/> belongs to if it has one
        /// </summary>
        private Album album;

        /// <summary>
        /// <see cref="Genre"/> of the <see cref="Song"/>
        /// </summary>
        private Genre genre;

        /// <summary>
        /// Formats a string containing information about a given <see cref="Song"/>
        /// </summary>
        /// <returns><see cref="Song"/>'s relevant information as a string</returns>
        public override string ToString()
        {
            return $"Song Name: {this.Name}\n" +
                $"Artist: {this.Artist}\n" +
                $"Duration: {this.Duration / 60} minutes and {this.Duration % 60} seconds\n" +
                $"Genre: {this.genre}\n" +
                $"Album Info:\n{this.album.ToString()}";
        }

        /// <summary>
        /// Explicit Constructor for <see cref="Song"/>
        /// </summary>
        /// <param name="inName"><see cref="String"/> representation of song Name</param>
        /// <param name="inLength"><see cref="int"/> representing length of song in seconds</param>
        /// <param name="inArtist"><see cref="String"/> representation of Artist Name</param>
        /// <param name="inAlbum"><see cref="Album"/> the <see cref="Song"/> belongs to if it has one</param>
        /// <param name="inGenre"><see cref="Genre"/> of the <see cref="Song"/></param>
        public Song(string inName, int inLength, string inArtist, Album inAlbum, Genre inGenre)
        {
            this.Name = inName;
            this.Duration = inLength;
            this.Artist = inArtist;
            this.album = inAlbum;
            this.genre = inGenre;
        }

        /// <summary>
        /// Constructor for <see cref="Song"/> that does not have an Album
        /// </summary>
        /// <param name="inName"><see cref="String"/> representation of song Name</param>
        /// <param name="inLength"><see cref="int"/> representing length of song in seconds</param>
        /// <param name="inArtist"><see cref="String"/> representation of Artist Name</param>
        /// <param name="inGenre"><see cref="Genre"/> of the <see cref="Song"/></param>
        public Song(string inName, int inLength, string inArtist, Genre inGenre) : this(inName, inLength, inArtist, new Album(), inGenre) { }
    }
}

