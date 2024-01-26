/**
*--------------------------------------------------------------------
* File name: Song.cs
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
    public class Song
    {
        public string name { get; private set; }
        public int songLength { get; private set; }
        public string artist { get; private set; }
        private Album album;
        private Genre genre;

        public override string ToString()
        {
            return $"Song Name: {this.name}\n" +
                $"Artist: {this.artist}\n" +
                $"Duration: {this.songLength}\n" +
                $"Album: {this.album.AlbumName}\n" +
                $"Genre: {this.genre}";
        }

        public Song(string inName, int inLength, string inartist, Album inAlbum, Genre inGenre)
        {
            this.name = inName;
            this.songLength = inLength;
            this.artist = inartist;
            this.album = inAlbum;
            this.genre = inGenre;
        }

        public Song() : this(String.Empty, 0, String.Empty, new Album(), Genre.Pop) { }
    }
}

