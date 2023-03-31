namespace MusicAPI.Models
{
    public class LikedAlbum
    {
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
