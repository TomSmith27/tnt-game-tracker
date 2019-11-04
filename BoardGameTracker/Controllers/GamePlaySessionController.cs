namespace BoardGameTracker.Controllers
{
    using BoardGameTracker.Models;
    using Database;
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/game-session")]
    [ApiController]
    public class GamePlaySessionController : ControllerBase
    {
        private readonly BoardGameContext db;

        public GamePlaySessionController(BoardGameContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            this.db.Players.Load();
            var gamePlaySessions = this.db.GamePlaySessions.Include(g => g.Game)
                .ThenInclude(g => g.PlayerRatings)
                .Include(p => p.Players)
                .Include(p => p.Winners);

            var gameSessions = gamePlaySessions.Select(g => new GamePlaySessionDto(g)).ToList().GroupBy(g => g.Date);


            return this.Ok(gameSessions.Select(g => new { g.Key, g }).OrderByDescending(o => o.Key));
        }

        [HttpGet("{sessionId:int}")]
        public IActionResult Get(int sessionId)
        {
            this.db.Players.Load();
            var gamePlaySession = this.db.GamePlaySessions
                .Include(g => g.Game)
                .Include(g => g.Players)
                .Include(g => g.Winners)
                .Single(g => g.Id == sessionId);

            return this.Ok(gamePlaySession);
        }

        [HttpPost("")]
        public IActionResult Create(CreateGamePlaySessionDto gameSession)
        {
            var players = db.Players.ToDictionary(p => p.Id);
            foreach (var player in gameSession.Players)
            {
                if (!players.ContainsKey(player))
                {
                    return this.BadRequest($"No player exists with id {player}");
                }
            }

            var session = gameSession.ToModel();
            db.GamePlaySessions.Add(session);

            var game = this.db.Games.Include(g => g.PlayerRatings).SingleOrDefault(g => g.Id == gameSession.GameId);
            foreach (var player in session.Players)
            {
                if (!game.PlayerRatings.Any(p => p.PlayerId == player.PlayerId))
                {
                    game.PlayerRatings.Add(new Models.PlayerRating()
                    {
                        PlayerId = player.PlayerId
                    });
                }
            }

            this.db.Activities.Add(new Activity
            {
                Date = DateTimeOffset.Now,
                Message = $"added {db.Games.Find(gameSession.GameId).Name} as a game session for {gameSession.Date.ToString("ddd, dd MMMM yyyy")} with {gameSession.Players.Count} players",
                PlayerId = int.Parse(this.HttpContext.User.Identity.Name)
            });


            db.SaveChanges();

            return this.Ok(session.Id);
        }

        [HttpPost("{id:int}")]
        public IActionResult Update(int id, CreateGamePlaySessionDto gameSession)
        {
            var players = db.Players.ToDictionary(p => p.Id);
            foreach (var player in gameSession.Players)
            {
                if (!players.ContainsKey(player))
                {
                    return this.BadRequest($"No player exists with id {player}");
                }
            }

            var session = this.db.GamePlaySessions
                .Include(p => p.Players)
                .Include(p => p.Winners)
                .Include(p => p.Game).ThenInclude(g => g.PlayerRatings)
                .Single(g => g.Id == id);
            session.Players.RemoveAll(p => true);
            session.Winners.RemoveAll(p => true);
            var sessionUpdate = gameSession.ToModel();
            session.Players = sessionUpdate.Players;
            session.Winners = sessionUpdate.Winners;
            session.GameId = sessionUpdate.GameId;
            session.Date = sessionUpdate.Date;
            session.Guests = sessionUpdate.Guests;
            foreach (var player in session.Players)
            {
                if (!session.Game.PlayerRatings.Any(p => p.PlayerId == player.PlayerId))
                {
                    session.Game.PlayerRatings.Add(new Models.PlayerRating()
                    {
                        PlayerId = player.PlayerId
                    });
                }
            }
            db.SaveChanges();

            return this.Ok(session.Id);
        }
    }
}
