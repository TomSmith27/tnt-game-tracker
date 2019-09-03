namespace BoardGameTracker.Models
{
    public class WishListEntry
    {
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public BoardGameEntry Game { get; set; }
        public int GameId { get; set; }
    }
}
