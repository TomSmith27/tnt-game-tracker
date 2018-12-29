using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Players);
            return this.Ok(gamePlaySessions);
        }

        [HttpGet("{sessionId:int}")]
        public IActionResult Get(int sessionId)
        {
            var gamePlaySession = this.db.GamePlaySessions
                .Include(g => g.Game)
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
            db.SaveChanges();

            return this.Ok(session.Id);
        }
    }
}
