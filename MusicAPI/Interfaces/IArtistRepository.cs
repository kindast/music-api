using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IArtistRepository
    {
        bool ArtistExists(int artistId);

        Artist GetArtist(int artistId);

        ICollection<Artist> GetArtists(string name);
    }
}
