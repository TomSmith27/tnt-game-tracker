using BoardGameTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Dto
{
    public class CreateGamePlaySessionDto
    {
        public int GameId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Notes { get; set; }
        public List<int> Players { get; set; }
        public List<int> Winners { get; set; }
        public int Guests { get; set; }

        public GamePlaySession ToModel()
        {
            return new GamePlaySession()
            {
                GameId = this.GameId,
                Date = this.Date,
                Notes = this.Notes,
                Players = this.Players.Select(p => new GamePlaySessionPlayer()
                {
                    PlayerId = p
                }).ToList(),
                Winners = this.Winners.Select(p => new GamePlaySessionWinner()
                {
                    PlayerId = p
                }).ToList(),
                Guests = this.Guests
            };
        }
    }
}
