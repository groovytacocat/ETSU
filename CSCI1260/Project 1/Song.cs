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
        public string Name { get; private set; }
        public int Duration { get; private set; }
        public string Artist { get; private set; }
        private Album album;
        private Genre genre;

        /// <summary>
        /// Formats a string containing information about a given <see cref="Song"/>
        /// </summary>
        /// <returns><see cref="Song"/>'s relevant information as a string</returns>
        public override string ToString()
        {
            return $"Song Name: {this.Name}\n" +
                $"Artist: {this.Artist}\n" +
                $"Duration: {this.Duration}\n" +
                $"Genre: {this.genre}\n" +
                $"\nAlbum Info:\n{this.album.ToString()}";
        }

        /// <summary>
        /// Explicit Constructor for <see cref="Song"/>
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="inLength"></param>
        /// <param name="inartist"></param>
        /// <param name="inAlbum"></param>
        /// <param name="inGenre"></param>
        public Song(string inName, int inLength, string inartist, Album inAlbum, Genre inGenre)
        {
            this.Name = inName;
            this.Duration = inLength;
            this.Artist = inartist;
            this.album = inAlbum;
            this.genre = inGenre;
        }

        /// <summary>
        /// Constructor for <see cref="Song"/> that does not have an Album
        /// </summary>
        /// <param Name="inName">Name of Song</param>
        /// <param Name="inLength">Length in seconds of song</param>
        /// <param Name="inArtist"Name of Artist>Name of the Artist</param>
        /// <param Name="inGenre">Genre of song</param>
        public Song(string inName, int inLength, string inArtist, Genre inGenre) : this(inName, inLength, inArtist, new Album(), inGenre) { }
    }
}

