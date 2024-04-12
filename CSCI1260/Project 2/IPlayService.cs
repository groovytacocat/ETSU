using System;
using PlaylistManger2.Data;

namespace PlaylistManger2.Services
{
    public interface IPlayService
    {
        public List<Data.Album> masterAlbum { get; set; }
        public List<Data.Song> masterSongs { get; set; }
        public List<Data.Playlist> masterPlaylist { get; set; }

        public void AddAlbum(Album input);
        public void AddPlaylist(Playlist playlist);
        public void AddSong(Song addSong);
    }
}