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
            this.Game = g.Game;
            this.PlayerSessionRatings = g.Players.Select(p => new PlayerSessionRatingDto(p, g.Game.PlayerRatings.SingleOrDefault(pr => pr.PlayerId == p.PlayerId)));
        }

        public BoardGameEntry Game { get; }
        public IEnumerable<PlayerSessionRatingDto> PlayerSessionRatings { get; }
    }
}
