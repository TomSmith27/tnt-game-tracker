namespace BoardGameTracker.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PlayerRating
    {
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public BoardGameEntry Game { get; set; }
        public int GameId { get; set; }
        [Range(0, 5)]
        public int? Rating { get; set; }
    }
}