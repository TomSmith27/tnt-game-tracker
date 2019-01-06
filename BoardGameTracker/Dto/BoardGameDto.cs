using BoardGameTracker.Models;
using System;
using System.Collections.Generic;

namespace BoardGameTracker.Dto
{
    public class BoardGameDto
    {
        public BoardGameDto(BoardGameEntry boardGame)
        {
            this.Id = boardGame.Id;
            this.Name = boardGame.Name;
            this.Description = boardGame.Description;
            this.Thumbnail = boardGame.Thumbnail;
            this.MinPlayers = boardGame.MinPlayers;
            this.MaxPlayers = boardGame.MaxPlayers;
            this.MinPlaytime = boardGame.MinPlaytime;
            this.MaxPlaytime = boardGame.MaxPlaytime;
            this.Image = boardGame.Image;
            this.ObjectId = boardGame.ObjectId;
            this.YearPublished = boardGame.YearPublished;
            this.AverageRating = boardGame.AverageRating;
            this.UsersRated = boardGame.UsersRated;
            this.Age = boardGame.Age;
            //this.Categories = boardGame.Categories;
        }
        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int ObjectId { get; }

        public DateTimeOffset YearPublished { get; }

        public int MinPlayers { get; }

        public int MaxPlayers { get; }

        public int PlayingTime { get; }

        public int MinPlaytime { get; }

        public int MaxPlaytime { get; }

        public int Age { get; }

        public string Image { get; }

        public string Thumbnail { get; }

        public double AverageRating { get; }

        public int UsersRated { get; }

        public List<BoardGameCategoryDto> Categories { get; } = new List<BoardGameCategoryDto>();

        public string BestPlayerCount { get; }
    }
}
