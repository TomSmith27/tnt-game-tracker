namespace BoardGameTracker.Controllers
{
    using Database;
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get()
        {
            this.db.Players.Load();
            var gamePlaySessions = this.db.GamePlaySessions.Include(g => g.Game)
                .ThenInclude(g => g.PlayerRatings)
                .Include(p => p.Players);

            var gameSessions = gamePlaySessions.Select(g => new GamePlaySessionDto(g));

            return this.Ok(gameSessions);
        }

        [HttpGet("{sessionId:int}")]
        public IActionResult Get(int sessionId)
        {
            this.db.Players.Load();
            var gamePlaySession = this.db.GamePlaySessions
                .Include(g => g.Game)
                .Include(g => g.Players)
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
                if(!game.PlayerRatings.Any(p => p.PlayerId == player.PlayerId))
                {
                    game.PlayerRatings.Add(new Models.PlayerRating()
                    {
                        PlayerId = player.PlayerId
                    });
                }
            }


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

            var session = this.db.GamePlaySessions.Include(p => p.Players).Single(g => g.Id == id);
            session.Players.RemoveAll(p => true);
            var sessionUpdate = gameSession.ToModel();
            session.Players = sessionUpdate.Players;
            session.GameId = sessionUpdate.GameId;
            session.Date = sessionUpdate.Date;
            db.SaveChanges();

            return this.Ok(session.Id);
        }
    }
}
