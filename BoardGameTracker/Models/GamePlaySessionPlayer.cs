namespace BoardGameTracker.Models
{
    public class GamePlaySessionPlayer
    {
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public GamePlaySession GamePlaySession{ get; set; }
        public int GamePlaySessionId { get; set; }
    }
}
