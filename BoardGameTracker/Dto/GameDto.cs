using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class GameDto
    {
        public GameDto(BoardGameEntry g, DateTimeOffset? from)
        {
            this.Id = g.Id;
            this.Name = g.Name;
            this.ObjectId = g.ObjectId;
            this.TimesPlayed = from.HasValue? g.Sessions.Count(s => s.Date > from) : g.Sessions.Count();
            this.LastPlayed = g.Sessions?.OrderByDescending(s => s.Date).FirstOrDefault()?.Date;
            this.AverageRating = g.AverageRating;
            this.PlayersAverageRating = g.PlayerRatings.Average(p => p.Rating);
            this.Thumbnail = g.Thumbnail;
        }

        public int Id { get; }
        public string Name { get; }
        public int ObjectId { get; }
        public int TimesPlayed { get; }
        public DateTimeOffset? LastPlayed { get; }
        public double AverageRating { get; }
        public double? PlayersAverageRating { get; }
        public string Thumbnail { get; }
    }
}
