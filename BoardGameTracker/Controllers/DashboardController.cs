﻿using BggApi.Service;
using BoardGameTracker.Database;
using BoardGameTracker.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoardGameTracker.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly BoardGameContext db;
        private readonly IBggService bggService;

        public DashboardController(BoardGameContext db, IBggService bggService)
        {
            this.db = db;
            this.bggService = bggService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var players = this.db.Players
               .Include(p => p.GamePlaySessions)
               .ThenInclude(s => s.GamePlaySession)
               .ThenInclude(g => g.Game)
               .Include(p => p.GamePlaySessions)
               .ThenInclude(s => s.GamePlaySession)
               .ThenInclude(g => g.Winners)
               .Include(p => p.Ratings)
               .ThenInclude(g => g.Game)
               .Include(p => p.GamePlayWins).ToList().Select(player => new PlayerDetailDto(player));

            return Ok(players);
        }
    }
}