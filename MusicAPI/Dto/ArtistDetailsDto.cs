using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class ArtistDetailsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public ICollection<AlbumRefDto>? Albums { get; set; }
    }
}
