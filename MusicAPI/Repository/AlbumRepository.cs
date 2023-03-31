using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicDbContext _context;

        public AlbumRepository(MusicDbContext context)
        {
            _context = context;
        }

        public bool AlbumExists(int albumId)
        {
            return _context.Albums.Any(a => a.Id == albumId);
        }

        public bool AlbumLiked(int albumId, int userId)
        {
            return _context.LikedAlbums.Where(a => a.AlbumId == albumId).Any(a => a.UserId == userId);
        }

        public Album GetAlbum(int albumId)
        {
            return _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Type)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Artists)
                .Where(a => a.Id == albumId)
                .FirstOrDefault();
        }

        public ICollection<Album> GetAlbums(string name)
        {
            return _context.Albums
                .Include(a => a.Artist)
                .Where(a => a.Name.Replace(" ", "").ToLower().Contains(name.Replace(" ", "").ToLower()))
                .ToList();
        }

        public ICollection<Album> GetLikedAlbums(int userId)
        {
            return _context.LikedAlbums
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreationDate)
                .Include(a => a.Album.Artist)
                .Select(a => a.Album)
                .ToList();
        }

        public bool ToggleLikedAlbum(int albumId, int userId)
        {
            if (AlbumLiked(albumId, userId))
            {
                var likedAlbum = _context.LikedAlbums.Where(a => a.AlbumId == albumId && a.UserId == userId).FirstOrDefault();
                _context.Remove(likedAlbum);
            }
            else
            {
                var likedAlbum = new LikedAlbum() { UserId = userId, AlbumId = albumId, CreationDate = DateTime.UtcNow };
                _context.Add(likedAlbum);
            }
            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
