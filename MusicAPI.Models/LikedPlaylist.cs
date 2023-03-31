namespace MusicAPI.Models
{
    public class LikedPlaylist
    {
        public int UserId { get; set; }
        public int PlaylistId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
