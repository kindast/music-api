namespace MusicAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
        public ICollection<Album>? Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
