namespace MusicAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AlbumType Type { get; set; }
        public string? Cover { get; set; }
        public string? Color { get; set; }
        public int Year { get; set; }
        public Artist? Artist { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public ICollection<LikedAlbum>? LikedUsers { get; set; }
    }
}
