using BoardGameTracker.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BoardGameTracker.Dto
{
    public class PlayerDetailDto
    {
        public PlayerDetailDto(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.Colour = "#" + player.Colour.R.ToString("X2") + player.Colour.G.ToString("X2") + player.Colour.B.ToString("X2");
            var playerRatings = player.Ratings.Where(r => r.Rating.HasValue);
            if (playerRatings.Any()) {
                this.AverageRating = playerRatings.Average(a => a.Rating.Value);
                this.AverageDifferenceToBGG = playerRatings.Average(r => r.Rating.Value - Math.Round(r.Game.AverageRating, 0));
            }

            var ratingsDistributionGrouped = playerRatings.Select(r => r.Rating.Value).GroupBy(g => g);
            var ratingsDistribution = Enumerable.Range(1, 10).Select((i) => new { i }).ToDictionary(x => x.i, x => 0);
            foreach (var rating in ratingsDistributionGrouped)
            {
                ratingsDistribution[rating.Key] = rating.Count();
            }

            this.RatingsDistribution = ratingsDistribution;

            var gamesPlayed = player.GamePlaySessions.Select(g => g.GamePlaySession.GameId);
            this.TotalGamesPlayed = gamesPlayed.Count();
            this.UniqueGamesPlayed = gamesPlayed.Distinct().Count();

            var gamesPlayedWhereThereWasAWinner = player.GamePlaySessions.Where(g => g.GamePlaySession.Winners.Any() && (g.GamePlaySession.Players.Count + g.GamePlaySession.Guests) != g.GamePlaySession.Winners.Count).Count();
            if (gamesPlayedWhereThereWasAWinner > 0)
            {
                this.GamesWonPercentage = ((double)player.GamePlayWins.Where(g => g.GamePlaySession.Winners.Any() && (g.GamePlaySession.Players.Count + g.GamePlaySession.Guests) != g.GamePlaySession.Winners.Count).Count() / gamesPlayedWhereThereWasAWinner) * 100;
            }

            if (player.Ratings != null && player.Ratings.Any())
            {
                var top3HighestRatings = player.Ratings.Where(r => r.Rating.HasValue).OrderByDescending(r => r.Rating.Value).Take(3);
                this.HighestRatedGames = top3HighestRatings.Select(t => new BoardGamePlayerRatingInfoDto(t.Game, t.Rating.Value));

                var top3LowestRatings = player.Ratings.Where(r => r.Rating.HasValue).OrderBy(r => r.Rating.Value).Take(3);
                this.LowestRatedGames = top3LowestRatings.Select(t => new BoardGamePlayerRatingInfoDto(t.Game, t.Rating.Value));
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
        public double GamesWonPercentage { get; set; }
        public IEnumerable<BoardGamePlayerRatingInfoDto> HighestRatedGames { get; }
        public IEnumerable<BoardGamePlayerRatingInfoDto> LowestRatedGames { get; }
        public BoardGamePlayerPlaysInfoDto MostPlayedGame { get; }
        public BoardGamePlayerPlaysInfoDto LeastPlayedGame { get; }
        public double AverageRating { get; }
        public string Colour { get; }
        public Dictionary<int, int> RatingsDistribution { get; }
        public double AverageDifferenceToBGG { get; }
    }
}
