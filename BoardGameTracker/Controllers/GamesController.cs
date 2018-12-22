using BggApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Controllers
{
    using System.Collections.Generic;
    using BoardGame.Api.Dto;
    using BoardGameTracker.Database;
    using BoardGameTracker.Models;
    using Microsoft.EntityFrameworkCore;

    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly BoardGameContext db;
        private readonly IBggService bggService;

        public GamesController(BoardGameContext db, IBggService bggService)
        {
            this.db = db;
            this.bggService = bggService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return this.Ok(this.db.Games.ToList());
        }

        [HttpGet("search-import")]
        [Produces(typeof(List<ImportableGame>))]
        public async Task<IActionResult> GetWithImported(string search, bool exact = false)
        {
            var result = await this.bggService.Search(search, exact);
            var currentGames = db.Games.ToDictionary(g => g.ObjectId);
            List<ImportableGame> importableGames = new List<ImportableGame>();

            foreach (var searchResult in result.Boardgames)
            {
                importableGames.Add(currentGames.ContainsKey(searchResult.Objectid)
                    ? new ImportableGame(currentGames[searchResult.Objectid])
                    : new ImportableGame(searchResult));
            }

            return this.Ok(importableGames);

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var game = this.db.Games
                .Include(g => g.Categories)
                .FirstOrDefault(g => g.Id == id);


            return this.Ok(game);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(BgObject bgObject)
        {
            var game = await this.bggService.Get(bgObject.ObjectId);

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

            return this.Ok(bgObject.ObjectId);
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