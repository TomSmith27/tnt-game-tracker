using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class GamePlaySessionDto
    {
        public GamePlaySessionDto(GamePlaySession g)
        {
            this.Id = g.Id;
            this.Game = g.Game;
            this.Date = g.Date;
            this.BggAverageRating = g.Game.AverageRating;
            this.PlayerAverageRating = g.Game.PlayerRatings.Average(a => a.Rating);
            this.Players = g.Players;
        }

        public int Id { get;  }
        public BoardGameEntry Game { get; }
        public DateTimeOffset Date { get; }
        public double BggAverageRating { get; }
        public double? PlayerAverageRating { get; }
        public List<GamePlaySessionPlayer> Players { get; }
    }
}
