using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface ISongRepository
    {
        bool SongExists(int songId);

        bool SongLiked(int songId, int userId);

        bool ToggleLikedSong(int songId, int userId);

        ICollection<Song> GetLikedSongs(int userId);

        ICollection<Song> GetSongs(string name);
    }
}
