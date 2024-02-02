/**
*--------------------------------------------------------------------
* File Name: SongList.cs
* Project Name: Playlist Manager
* Solution Name: Playlist Manager
*--------------------------------------------------------------------
* Author’s Name and email: Adam Hooven, hoovenar@etsu.edu
* Course-Section: CSCI 1260 - 077
* Creation Date: 01/25/2024
* Modified Date: 01/26/2024
* -------------------------------------------------------------------
*/

using System;
namespace PlaylistManager
{
    /// <summary>
    /// A type of Object that contains a colelction of songs
    /// </summary>
    public abstract class SongList
    {
        /// <summary>
        /// A List of <see cref="Song"/> objects
        /// </summary>
        protected List<Song> songList;

        /// <summary>
        /// The total duration of the <see cref="SongList"/> Object's <see cref="Song"/>s
        /// </summary>
        protected int runTime;

        /// <summary>
        /// Name of the <see cref="SongList"/> Object
        /// </summary>
        protected string title;

        /// <summary>
        /// Adds a <see cref="Song"/> to a <see cref="List{Song}"/>
        /// </summary>
        /// <param Name="song"><see cref="Song"/> to be added</param>
        /// <returns>Returns a <see cref="List{Song}"/></returns>
        public abstract List<Song> AddSong(Song song);

        /// <summary>
        /// Prints the information of a <see cref="Song"/> in the <see cref="List{T}"/> of <see cref="Song"/>s
        /// </summary>
        /// <param name="songNum"><see cref="int"/> representing a <see cref="Song"/>'s index value in the <see cref="List{T}"/></param>
        /// <returns><see cref="String"/> representing that <see cref="Song"/>'s information</returns>
        public abstract string GetSong(int songNum);

        /// <summary>
        /// Explicit Constructor for <see cref="SongList"/>
        /// </summary>
        /// <param Name="inSongList"><see cref="List{T}"/> of <see cref="Song"/>s</param>
        /// <param Name="inRunTime">Length in seconds of <see cref="SongList"/></param>
        /// <param Name="inTitle">Title of <see cref="SongList"/></param>
        public SongList(List<Song> inSongList, int inRunTime, string inTitle)
        {
            this.songList = inSongList;
            this.runTime = inRunTime;
            this.title = inTitle;
        }

        /// <summary>
        /// Creates an instance of <see cref="SongList"/>
        /// </summary>
        public SongList(string inTitle) : this(new List<Song>(), 0, inTitle) { }
    }
}

