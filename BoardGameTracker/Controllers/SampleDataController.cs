using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Database;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameTracker.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly BoardGameContext db;
        public SampleDataController(BoardGameContext boardGameContext)
        {
            this.db = boardGameContext;
        }

        [HttpGet("[action]")]
        public IActionResult WeatherForecasts()
        {
            return this.Ok(db.Players.ToList());
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
