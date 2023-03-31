using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class MusicDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<LikedSong> LikedSongs { get; set; }
        public DbSet<LikedPlaylist> LikedPlaylists { get; set; }
        public DbSet<LikedAlbum> LikedAlbums { get; set; }

        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LikedSong>()
                .HasKey(l => new { l.SongId, l.UserId });
            builder.Entity<LikedPlaylist>()
                .HasKey(l => new { l.PlaylistId, l.UserId });
            builder.Entity<LikedAlbum>()
                .HasKey(l => new { l.AlbumId, l.UserId });
            base.OnModelCreating(builder);
        }
    }
}
