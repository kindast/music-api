namespace MusicAPI.Models
{
    public class LikedSong
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public Song? Song { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
