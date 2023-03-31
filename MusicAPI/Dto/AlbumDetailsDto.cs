using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class AlbumDetailsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public AlbumType Type { get; set; }
        public string? Cover { get; set; }
        public string? Color { get; set; }
        public int Year { get; set; }
        public bool IsLiked { get; set; }
        public ArtistDto? Artist { get; set; }
        public ICollection<SongDto>? Songs { get; set; }
    }
}
