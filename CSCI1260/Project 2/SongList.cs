using System;
namespace PlaylistManger2.Data
{
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
        public abstract void AddSong(Song song);

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

