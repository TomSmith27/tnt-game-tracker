namespace BoardGameTracker.Dto
{
    using BoardGameTracker.Models;
    using System.Linq;

    public class PlayerStatsDto : PlayerDto
    {
        public PlayerStatsDto(Player player) : base(player)
        {
            this.GamesPlayed = player.GamePlaySessions.Count();
            this.UnratedGames = player.Ratings.Count(r => !r.Rating.HasValue);
            this.RatedGames = player.Ratings.Count(r => r.Rating.HasValue);
        }
        public int GamesPlayed { get;  }
        public int UnratedGames { get; }
        public int RatedGames { get; }
        public int UniqueGamesPlayed => this.UnratedGames + this.RatedGames;
    }
}
