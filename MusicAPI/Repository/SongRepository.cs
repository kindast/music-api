using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDbContext _context;

        public SongRepository(MusicDbContext context)
        {
            _context = context;
        }

        public ICollection<Song> GetLikedSongs(int userId)
        {
            return _context.LikedSongs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreationDate)
                .Include(a => a.Song.Artists)
                .Include(a => a.Song.Album)
                .Select(a => a.Song)
                .ToList();
        }

        public ICollection<Song> GetSongs(string name)
        {
            return _context.Songs
                .Include(a => a.Artists)
                .Where(a => a.Name.Replace(" ", "").ToLower().Contains(name.Replace(" ", "").ToLower()))
                .ToList();
        }

        public bool SongExists(int songId)
        {
            return _context.Songs.Any(s => s.Id == songId);
        }

        public bool SongLiked(int songId, int userId)
        {
            return _context.LikedSongs.Where(s => s.SongId == songId).Any(s => s.UserId == userId);
        }

        public bool ToggleLikedSong(int songId, int userId)
        {
            if (SongLiked(songId, userId))
            {
                var likedSong = _context.LikedSongs.Where(a => a.SongId == songId && a.UserId == userId).FirstOrDefault();
                _context.Remove(likedSong);
            }
            else
            {
                var likedSong = new LikedSong() { UserId = userId, SongId = songId, CreationDate = DateTime.UtcNow };
                _context.Add(likedSong);
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
