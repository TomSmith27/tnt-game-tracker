using BoardGameTracker.Models;
using System.Linq;

namespace BoardGameTracker.Dto
{
    public class PlayerDetailDto
    {
        public PlayerDetailDto(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            var gamesPlayed = player.GamePlaySessions.Select(g => g.GamePlaySession.GameId);
            this.TotalGamesPlayed = gamesPlayed.Count();
            this.UniqueGamesPlayed = gamesPlayed.Distinct().Count();

            if (player.Ratings != null && player.Ratings.Any())
            {
                var highestRating = player.Ratings.Where(r => r.Rating.HasValue).OrderByDescending(r => r.Rating.Value).First();
                this.HighestRatedGame = new BoardGamePlayerRatingInfoDto(highestRating.Game, highestRating.Rating.Value);

                var lowestRating = player.Ratings.Where(r => r.Rating.HasValue).OrderBy(r => r.Rating.Value).First();
                this.LowestRatedGame = new BoardGamePlayerRatingInfoDto(lowestRating.Game, lowestRating.Rating.Value);
            }
            if (player.GamePlaySessions != null && player.GamePlaySessions.Any())
            {
                var mostPlayedGameGroup = player.GamePlaySessions.GroupBy(g => g.GamePlaySession.GameId).OrderByDescending(g => g.Count()).FirstOrDefault();
                var mostPlayedGame = mostPlayedGameGroup.FirstOrDefault().GamePlaySession.Game;
                this.MostPlayedGame = new BoardGamePlayerPlaysInfoDto(mostPlayedGame, mostPlayedGameGroup.Count());

                var leastPlayedGameGroup = player.GamePlaySessions.GroupBy(g => g.GamePlaySession.GameId).OrderBy(g => g.Count()).FirstOrDefault();
                var leastPlayedGame = leastPlayedGameGroup.FirstOrDefault().GamePlaySession.Game;
                this.LeastPlayedGame = new BoardGamePlayerPlaysInfoDto(leastPlayedGame, leastPlayedGameGroup.Count());
            }

        }
        public int Id { get; }
        public string Name { get; }
        public int UniqueGamesPlayed { get; }
        public int TotalGamesPlayed { get; }
        public BoardGamePlayerRatingInfoDto HighestRatedGame { get; }
        public BoardGamePlayerRatingInfoDto LowestRatedGame { get; }
        public BoardGamePlayerPlaysInfoDto MostPlayedGame { get; }
        public BoardGamePlayerPlaysInfoDto LeastPlayedGame { get; }
    }
}
