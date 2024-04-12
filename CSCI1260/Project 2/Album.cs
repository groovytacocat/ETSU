using System;
using System.ComponentModel.DataAnnotations;

namespace PlaylistManger2.Data
{
    /// <summary>
    /// Represents a type of <see cref="SongList"/> that has a collection of <see cref="Song"/>s 
    /// by the same artist.
    /// </summary>
    public class Album : SongList
    {
        /// <summary>
        /// <see cref="List{T}"/> of <see cref="String"/>s containing names of Band Members (if any)
        /// </summary>
        public List<string> BandMembers { get; private set; }

        /// <summary>
        /// <see cref="String"/> representation of Band Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Please enter a band/artist name", MinimumLength = 1)]
        public string BandName { get; private set; }

        /// <summary>
        /// <see cref="String"/> representation of URL for album art
        /// Paramter checks that the input is a valid URL beginning with http or https <--- Found online @https://www.makeuseof.com/regular-expressions-validate-url/
        /// </summary>
        [Required]
        [RegularExpression("^((http|https)://)[-a-zA-Z0-9@:%._\\+~#?&//=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%._\\+~#?&//=]*)$", ErrorMessage = "Must enter a valid url beginning with http(s)")]
        public string ArtUrl { get; private set; }

        /// <summary>
        /// <see cref="int"/> representing the year released
        /// </summary>
        [Required]
        [Range(1889, 2024, ErrorMessage = "Release Date must be between 1889 and 2024")]
        public int ReleaseDate { get; private set; }

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="Song"/>s for the <see cref="Album"/>
        /// </summary>
        public List<Song> TrackList { get { return this.songList; } }

        /// <summary>
        /// <see cref="String"/> representation of the Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Please enter an Album name", MinimumLength = 1)]
        public string Name { get { return this.title; } }

        /// <summary>
        /// Length of Album as an <see cref="int"/> in seconds
        /// </summary>
        [Required]
        [Range(1, 3600, ErrorMessage = "Please enter a value between 1 and 3600")]
        public int Length { get { return this.runTime; } }

        /// <summary>
        /// <see cref="Genre"/> of the <see cref="Song"/>
        /// </summary>
        [Required(ErrorMessage = "Please select a genre from the provided list")]
        public Genre Genre { get; private set; }

        /// <summary>
        /// <see cref="bool"/> for use to display more detailed table row information
        /// </summary>
        public bool MoreInfo { get; set; } = false;

        /// <inheritdoc />
        public override void AddSong(Song song)
        {
            this.songList.Add(song);

            this.runTime += song.Duration;
        }

        /// <summary>
        /// Constructor for <see cref="Album"/>
        /// </summary>
        /// <param Name="albumName">Name of Album as a <see cref="String"/></param>
        /// <param Name="BandName">Name of band as a <see cref="String"/></param>
        /// <param Name="ReleaseDate">Year album was released as an <see cref="int"/> </param>
        public Album(string albumName, string bandName, List<string> inMembers, int releaseDate, Genre genre, string inURL) : base(albumName)
        {
            this.BandMembers = inMembers;
            this.BandName = bandName;
            this.ReleaseDate = releaseDate;
            this.Genre = genre;
            this.ArtUrl = inURL;
        }

        /// <summary>
        /// Default Constructor creates an album with no name/band members for <see cref="Song"/>s that aren't tied to an album
        /// </summary>
        public Album() : this("", "", new List<string>(), 0, Genre.Other,"") { }
    }
}

