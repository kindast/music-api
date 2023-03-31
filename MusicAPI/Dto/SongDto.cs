using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class SongDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Source { get; set; }
        public bool IsLiked { get; set; }
        public AlbumRefDto? Album { get; set; }
        public ICollection<ArtistDto> Artists { get; set; }
    }
}