namespace MusicAPI.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cover { get; set; }
        public string? Color { get; set; }
        public User? Author { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public ICollection<LikedPlaylist>? LikedUsers { get; set; }
    }
}
