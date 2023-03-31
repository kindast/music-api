using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IPlaylistRepository
    {
        Playlist GetPlaylist(int playlistId);
        
        void ToggleLikedPlaylist(int playlistId, int userId);

        bool PlaylistExists(int playlistId);
    }
}
