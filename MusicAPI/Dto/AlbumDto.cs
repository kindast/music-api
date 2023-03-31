using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cover { get; set; }
        public ArtistDto? Artist { get; set; }
    }
}
