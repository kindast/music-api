namespace MusicAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Source { get; set; }
        public Album? Album { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Playlist> Playlists { get; set; }
        public ICollection<LikedSong>? LikedUsers { get; set; }
    }
}
