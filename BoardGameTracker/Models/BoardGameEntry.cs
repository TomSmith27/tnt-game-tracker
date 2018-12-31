namespace BoardGameTracker.Models
{
    using BggApi.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class BoardGameEntry
    {
        public BoardGameEntry()
        {

        }

        public BoardGameEntry(Boardgame boardGame)
        {
            this.Name = boardGame.Name.Single(n => n.Primary).Text;
            this.Description = boardGame.Description;
            this.Thumbnail = boardGame.Thumbnail;
            this.MinPlayers = boardGame.Minplayers;
            this.MaxPlayers = boardGame.Maxplayers;
            this.MinPlaytime = boardGame.Minplaytime;
            this.MaxPlaytime = boardGame.Maxplaytime;
            this.Image = boardGame.Image;
            this.ObjectId = boardGame.Objectid;
            this.YearPublished = new DateTimeOffset(new DateTime(boardGame.Yearpublished, 1, 1));
            this.AverageRating = boardGame.Statistics.Ratings.Average;
            this.UsersRated = boardGame.Statistics.Ratings.Usersrated;
            this.Age = boardGame.Age;
            var playerPoll = boardGame.Poll.FirstOrDefault(p => p.Name == "suggested_numplayers");
            if (playerPoll != null)
            {
                int highestResult = 0;
                foreach (var res in playerPoll.Results)
                {
                    var bestResult = res.Result.First(rn => rn.Value == "Best");
                    if (bestResult.Numvotes > highestResult)
                    {
                        highestResult = bestResult.Numvotes;
                        this.BestPlayerCount = res.Numplayers;
                    }
                }
            }
            

        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ObjectId { get; set; }

        public DateTimeOffset YearPublished { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int PlayingTime { get; set; }

        public int MinPlaytime { get; set; }

        public int MaxPlaytime { get; set; }

        public int Age { get; set; }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public double AverageRating { get; set; }

        public int UsersRated { get; set; }

        public List<GameCategoryGameEntry> Categories { get; set; } = new List<GameCategoryGameEntry>();

        public string BestPlayerCount { get; set; }

        public List<PlayerRating> PlayerRatings { get; set; }
        public List<GamePlaySession> Sessions { get; set; }
    }
}