using BggApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace BoardGameTracker.Controllers
{
    using BoardGame.Api.Dto;
    using BoardGameTracker.Database;
    using BoardGameTracker.Dto;
    using BoardGameTracker.Models;
    using BoardGameTracker.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly BoardGameContext db;
        private readonly IBggService bggService;
        private readonly IUserService userService;

        public GamesController(BoardGameContext db, IBggService bggService, IUserService userService)
        {
            this.db = db;
            this.bggService = bggService;
            this.userService = userService;
        }

        [HttpGet("")]
        public IActionResult Get([FromQuery] DateTimeOffset? from)
        {
            var games  = this.db.Games.Include(g => g.Sessions).Include(g => g.PlayerRatings).ToList();
            return this.Ok(games.Select(g => new GameDto(g, from)));

        }

        [HttpGet("search-import")]
        [Produces(typeof(List<ImportableGame>))]
        public async Task<IActionResult> GetWithImported(string search, bool exact = false)
        {
            var result = await this.bggService.Search(search, exact);
            var currentGames = db.Games.ToDictionary(g => g.ObjectId);
            List<ImportableGame> importableGames = new List<ImportableGame>();

            foreach (var searchResult in result.Boardgames.Where(g => g.Name.Any(s => s.Primary)))
            {
                importableGames.Add(currentGames.ContainsKey(searchResult.Objectid)
                    ? new ImportableGame(currentGames[searchResult.Objectid])
                    : new ImportableGame(searchResult));
            }

            return this.Ok(importableGames.OrderByDescending(s => s.YearPublished));

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            this.db.Players.Load();
            this.db.Categories.Load();
            var game = this.db.Games
                .Include(g => g.Categories)
                .Include(g => g.PlayerRatings)
                .FirstOrDefault(g => g.Id == id);
            bool isOnWishList = db.WishList.Any(a =>
                a.PlayerId == int.Parse(this.HttpContext.User.Identity.Name) && a.GameId == id);

            return this.Ok(new {game, isOnWishList});
        }

        [HttpGet("player-ratings")]
        public IActionResult PlayerRating(int? userId = null)
        {
            if (!userId.HasValue)
            {
                userId = int.Parse(this.HttpContext.User.Identity.Name);
            }
            var playerRatings = this.db.Players
                .Include(s => s.Ratings)
                .ThenInclude(rating => rating.Game)
                .Single(p => p.Id == userId);

            return this.Ok(playerRatings);           
        }

        [HttpGet("player-unrated-games")]
        public IActionResult PlayerUnratedGamesCount()
        {
            var userId = int.Parse(this.HttpContext.User.Identity.Name);
            var unratedGames = this.db.Players.Where(p => p.Id == userId).Select(p => p.Ratings.Count(r => !r.Rating.HasValue)).Sum();
            return this.Ok(unratedGames);
        }


        [HttpPost("")]
        public async Task<IActionResult> Create(BgObject bgObject)
        {
            var game = await this.bggService.Get(bgObject.ObjectId);

            if(db.Games.Any(g => g.ObjectId == bgObject.ObjectId))
            {
                return BadRequest("Already Imported This game");
            }


            var categories = this.db.Categories.ToDictionary(c => c.ObjectId, c => c);

            var boardGame = new BoardGameEntry(game);

            foreach (var importCategory in game.Boardgamecategory)
            {
                //If the category doesn't exist create it
                if (!categories.ContainsKey(importCategory.Objectid))
                {
                    boardGame.Categories.Add(
                        new GameCategoryGameEntry()
                        {
                            BoardGameCategory = new BoardGameCategory
                            {
                                Name = importCategory.Text,
                                ObjectId = importCategory.Objectid,
                            }
                        }
                    );
                }
                else
                {
                    boardGame.Categories.Add(new GameCategoryGameEntry
                    {
                        BoardGameCategory = categories[importCategory.Objectid]
                    }
                    );
                }
            }

            this.db.Games.Add(boardGame);

            this.db.SaveChanges();

            return this.Ok(boardGame.Id);
        }

        [HttpPost("{id:int}/rate")]
        public async Task<IActionResult> Rate(int id, RatingDto dto)
        {
            var userId = int.Parse(this.HttpContext.User.Identity.Name);
            var user = userService.GetById(userId);
            var rating = this.db.Ratings.First(r => r.GameId == id && r.PlayerId == user.Id);
            this.db.Activities.Add(new Activity
            {
                Date = DateTimeOffset.Now,
                Message = $"changed {db.Games.Find(id).Name}'s rating from {rating.Rating} to {dto.Rating}",
                PlayerId = userId
            });

            rating.Rating = dto.Rating;
            
          


            await db.SaveChangesAsync();

            return this.Ok();

        }

        [HttpPost("{id:int}/add-to-wishlist")]
        public async Task<IActionResult> AddToWishList(int id)
        {
            var userId = int.Parse(this.HttpContext.User.Identity.Name);
            this.db.WishList.Add(new WishListEntry()
            {
                GameId = id,
                PlayerId = userId
            });
            await db.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost("{id:int}/remove-from-wishlist")]
        public async Task<IActionResult> RemoveFromWishList(int id)
        {
            var userId = int.Parse(this.HttpContext.User.Identity.Name);
            var wishList = this.db.WishList.First(w => w.PlayerId == userId && w.GameId == id);
            this.db.WishList.Remove(wishList);
            await db.SaveChangesAsync();

            return this.Ok();
        }

        [HttpPost("import")]
        public IActionResult ReImport()
        {
            //Todo Reimort?
            return this.Ok();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            int[] games = new[]
            {
                230802,
                174430,
                170216,
                31260
            };
            foreach (var game in games)
            {
                await this.Create(new BgObject()
                {
                    ObjectId = game
                });
            }


            return this.Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            this.db.Games.Remove(this.db.Games.Single(b => b.Id == id));
            await this.db.SaveChangesAsync();

            return this.Ok();
        }


    }
}