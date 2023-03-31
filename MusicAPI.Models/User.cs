namespace MusicAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<LikedPlaylist>? LikedAlbums { get; set; }
        public ICollection<LikedSong>? LikedSongs { get; set; }
    }
}
