using BoardGameTracker.Database;
using BoardGameTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BoardGameTracker.Dto;

namespace BoardGameTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly BoardGameContext db;
        private readonly IUserService userService;

        public WishlistController(BoardGameContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var wishLists = this.db.WishList
                .Include(w => w.Game)
                .Include(w => w.Player)
                .GroupBy(w => w.Game);

            var wishListRanks = wishLists.Select(w => new GameWishListRanking(w));

            return this.Ok(wishListRanks.OrderByDescending(w => w.Players.Count));
        }
    }
}
