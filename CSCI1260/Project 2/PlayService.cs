using System;
using PlaylistManger2.Data;
using PlaylistManger2.Pages;
using PlaylistManger2.Shared;

namespace PlaylistManger2.Services
{
    public class PlayService : IPlayService
    {
        public List<Data.Album> masterAlbum { get; set; } = new List<Data.Album>();
        public List<Data.Song> masterSongs { get; set; } = new List<Data.Song>();
        public List<Data.Playlist> masterPlaylist { get; set; } = new List<Data.Playlist>();

        public void AddAlbum(Album input)
        {
            masterAlbum.Add(input);
        }

        public void AddPlaylist(Playlist playlist)
        {
            masterPlaylist.Add(playlist);
        }

        public void AddSong(Song addSong)
        {
            masterSongs.Add(addSong);
        }

    }
}

