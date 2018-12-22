using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BoardGameTracker.Controllers
{
    using BggApi.Service;

    [Route("api/bgg")]
    [ApiController]
    public class BGGApiController : ControllerBase
    {
        private readonly IBggService bggService;

        public BGGApiController(IBggService bggService)
        {
            this.bggService = bggService;
        }

        [HttpGet("{objectId:int}")]
        public async Task<IActionResult> Get(int objectId)
        {
            try
            {
                var boardGame = await this.bggService.Get(objectId);
                //return this.Ok(result);
                return this.Ok(boardGame);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string search, bool exact = false)
        {
            try
            {
                var result = await this.bggService.Search(search, exact);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

        }
    }
}