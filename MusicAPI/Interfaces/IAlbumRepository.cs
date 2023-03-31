using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IAlbumRepository
    {
        Album GetAlbum(int albumId);

        ICollection<Album> GetLikedAlbums(int userId);

        ICollection<Album> GetAlbums(string name);

        bool ToggleLikedAlbum(int albumId, int userId);

        bool AlbumExists(int albumId);

        bool AlbumLiked(int albumId, int userId);
    }
}
