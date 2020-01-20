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
            this.Game = new BoardGameDto(g.Game);
            this.Date = g.Date;
            this.BggAverageRating = g.Game.AverageRating;
            this.PlayerAverageRating = g.Game.PlayerRatings.Average(a => a.Rating);
            this.Players = g.Players.OrderBy(p => p.Player.Name).Select(p => new PlayerSessionDto(p.Player, g.Winners.Any(w => w.PlayerId == p.PlayerId))).ToList();
            this.Guests = g.Guests;
            this.Notes = g.Notes;
        }

        public int Id { get;  }
        public BoardGameDto Game { get; }
        public DateTimeOffset Date { get; }
        public double BggAverageRating { get; }
        public double? PlayerAverageRating { get; }
        public List<PlayerSessionDto> Players { get; }
        public int Guests { get; }
        public string Notes { get; }
    }
}
