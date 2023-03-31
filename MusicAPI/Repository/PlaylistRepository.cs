using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MusicDbContext _context;

        public PlaylistRepository(MusicDbContext context)
        {
            _context = context;
        }

        public Playlist GetPlaylist(int playlistId)
        {
            return _context.Playlists
                .Include(p => p.Songs)
                .Where(p => p.Id == playlistId)
                .FirstOrDefault();
        }

        public void ToggleLikedPlaylist(int playlistId, int userId)
        {
            var likedPlaylist = _context.LikedPlaylists.Where(p => p.PlaylistId == playlistId && p.UserId == userId).FirstOrDefault();  
            if (likedPlaylist == null)
                _context.LikedPlaylists.AddAsync(new LikedPlaylist() 
                {   UserId = userId,
                    PlaylistId = playlistId,
                    CreationDate = DateTime.UtcNow
                });
            else
                _context.LikedPlaylists.Remove(likedPlaylist);
            _context.SaveChangesAsync();
        }

        public bool PlaylistExists(int id)
        {
            var playlist = _context.Playlists.Where(p => p.Id == id).FirstOrDefault();
            return playlist != null ? true : false;
        }
    }
}
