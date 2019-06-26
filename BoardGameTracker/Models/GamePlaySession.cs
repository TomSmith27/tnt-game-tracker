namespace BoardGameTracker.Models
{
    using System;
    using System.Collections.Generic;

    public class GamePlaySession
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public BoardGameEntry Game { get; set; }
        public int GameId { get; set; }
        public int Guests { get; set; }
        public List<GamePlaySessionPlayer> Players { get; set; } = new List<GamePlaySessionPlayer>();
        public List<GamePlaySessionWinner> Winners { get; set; } = new List<GamePlaySessionWinner>();
    }
}
