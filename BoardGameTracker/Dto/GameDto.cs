﻿using BoardGameTracker.Models;
using System;
using System.Linq;

namespace BoardGameTracker.Dto
{
    public class GameDto
    {
        public GameDto(BoardGameEntry g, DateTimeOffset? from = null)
        {
            this.Id = g.Id;
            this.Name = g.Name;
            this.ObjectId = g.ObjectId;
            this.TimesPlayed = from.HasValue ? g.Sessions.Count(s => s.Date > from) : g.Sessions.Count();
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
        public double? Difference => this.PlayersAverageRating - this.AverageRating;
        public string Thumbnail { get; }
    }
}
