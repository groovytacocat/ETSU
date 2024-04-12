using System;
namespace PlaylistManger2.Data
{
    public class Playlist : SongList
    {
        public List<Song> TrackList { get { return this.songList; } }
        public string Name { get { return this.title; } }
        public int Duration { get { return this.runTime; } }


        /// <inheritdoc/>
        public override void AddSong(Song song)
        {
            this.songList.Add(song);
            runTime += song.Duration;
        }


        /// <summary>
        /// Removes a <see cref="Song"/> from the <see cref="Playlist"/> and updates the total runtime of the <see cref="Playlist"/>
        /// </summary>
        /// <param name="song"><see cref="Song"/> to be removed</param>
        /// <returns>Returns the <see cref="List{T}"/> of <see cref="Song"/>s in the <see cref="Playlist"/>  </returns>
        public void RemoveSong(Song song)
        {
            this.songList.Remove(song);
            runTime -= song.Duration;
        }

        /// <summary>
        /// Shuffles the elements of the <see cref="List{T}"/>
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            int maxSwapIndex = this.TrackList.Count - 1;

            for (int i = maxSwapIndex; i > 1; i--)
            {
                int swapIndex = random.Next(0, i + 1);

                Song swap = this.TrackList[swapIndex];

                this.TrackList[swapIndex] = this.TrackList[i];

                this.TrackList[i] = swap;
            }

            if(this.TrackList.Count == 2)
            {
                int swapIndex = 1;
                Song swap = this.TrackList[swapIndex];
                this.TrackList[swapIndex] = this.TrackList[0];
                this.TrackList[0] = swap;

            }
        }

        /// <summary>
        /// Constructor for a <see cref="Playlist"/>
        /// </summary>
        /// <param name="name">Name of the <see cref="Playlist"/></param>
        public Playlist(string name) : base(name) { }

        public Playlist(string name, List<Song> songs) : base(name)
        {
            this.songList = songs;
        }
    }
}

