using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Database;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameTracker.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly BoardGameContext db;

        public BaseController(BoardGameContext db)
        {
            this.db = db;
        }

        protected int UserId => int.Parse(this.HttpContext.User.Identity.Name);
        protected int CurrentYearFilter => this.db.Players.First(p => p.Id == this.UserId).CurrentYearFilter.Year;
    }
}
