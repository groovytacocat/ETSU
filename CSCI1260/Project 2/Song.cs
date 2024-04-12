using System;
using System.ComponentModel.DataAnnotations;

namespace PlaylistManger2.Data
{
    public class Song
    {
        /// <summary>
        /// <see cref="String"/> representation of song Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Please enter a song name", MinimumLength = 1)]
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="int"/> representing length of song in seconds
        /// </summary>
        [Required]
        [Range(1, 3600, ErrorMessage = "Please enter a value between 1 and 3600")]
        public int Duration { get; private set; }

        /// <summary>
        /// <see cref="String"/> representation of Artist Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Please enter a band/artist name", MinimumLength = 1)]
        public string Artist { get; private set; }

        /// <summary>
        /// <see cref="Album"/> the <see cref="Song"/> belongs to if it has one
        /// </summary>
        public Album album { get; private set; }

        /// <summary>
        /// Year Song was released as an <see cref="int"/>
        /// </summary>
        [Required]
        [Range(1889, 2024, ErrorMessage = "Release Date must be between 1889 and 2024")]
        public int ReleaseDate { get; private set; }

        /// <summary>
        /// <see cref="Genre"/> of the <see cref="Song"/>
        /// </summary>
        [Required(ErrorMessage = "Please select a genre from the provided list")]
        public Genre Genre { get; private set; }

        /// <summary>
        /// <see cref="bool"/> for use to display more detailed table row information
        /// </summary>
        public bool MoreInfo { get; set; } = false;

        /// <summary>
        /// Explicit Constructor for <see cref="Song"/>
        /// </summary>
        /// <param name="inName"><see cref="String"/> representation of song Name</param>
        /// <param name="inLength"><see cref="int"/> representing length of song in seconds</param>
        /// <param name="inArtist"><see cref="String"/> representation of Artist Name</param>
        /// <param name="inAlbum"><see cref="Album"/> the <see cref="Song"/> belongs to if it has one</param>
        /// <param name="inGenre"><see cref="Genre"/> of the <see cref="Song"/></param>
        public Song(string inName, int inLength, string inArtist, Album inAlbum, Genre genre, int inDate)
        {
            this.Name = inName;
            this.Duration = inLength;
            this.Artist = inArtist;
            this.album = inAlbum;
            this.Genre = genre;
            this.ReleaseDate = inDate;
        }

        /// <summary>
        /// Constructor for <see cref="Song"/> that does not have an Album
        /// </summary>
        /// <param name="inName"><see cref="String"/> representation of song Name</param>
        /// <param name="inLength"><see cref="int"/> representing length of song in seconds</param>
        /// <param name="inArtist"><see cref="String"/> representation of Artist Name</param>
        /// <param name="inGenre"><see cref="Genre"/> of the <see cref="Song"/></param>
        public Song(string inName, int inLength, string inArtist, Genre genre, int inDate) : this(inName, inLength, inArtist, new Album(), genre, inDate) { }

        public Song() : this("", 0, "", new Album(), Genre.Other, 0) { }
    }
}

