using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MusicDbContext _context;

        public ArtistRepository(MusicDbContext context) 
        {
            _context = context;
        }

        public bool ArtistExists(int artistId)
        {
            return _context.Artists.Any(a => a.Id == artistId);
        }

        public Artist GetArtist(int artistId)
        {
            return _context.Artists.Where(a => a.Id == artistId).Include(a => a.Albums).FirstOrDefault();
        }

        public ICollection<Artist> GetArtists(string name)
        {
            return _context.Artists
                .Where(a => a.Name.Replace(" ", "").ToLower().Contains(name.Replace(" ", "").ToLower()))
                .ToList();
        }
    }
}
