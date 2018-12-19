namespace BoardGameTracker.Models
{
    public class GameCategoryGameEntry
    {
        public BoardGameEntry BoardGameEntry { get; set; }
        public int BoardGameEntryId { get; set; }
        public BoardGameCategory BoardGameCategory { get; set; }
        public int BoardGameCategoryId { get; set; }

    }
}
