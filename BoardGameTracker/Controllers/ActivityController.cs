using BoardGameTracker.Database;
using BoardGameTracker.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoardGameTracker.Controllers
{
    [Route("api/activites")]
    public class ActivityController : ControllerBase
    {

        private readonly BoardGameContext db;

        public ActivityController(BoardGameContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var players = this.db.Activities
               .Include(p => p.Player).OrderBy(d => d.Date).ToList().Select(player => new ActivityDto(player));

            return Ok(players);
        }
    }
}
