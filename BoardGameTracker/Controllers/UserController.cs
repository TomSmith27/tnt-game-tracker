namespace BoardGameTracker.Controllers
{
    using BoardGameTracker.Database;
    using BoardGameTracker.Dto;
    using BoardGameTracker.Services;
    using BoardGameTracker.Settings;
    using Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private IUserService userService;
        private readonly AppSettings appSettings;

        public UsersController(
            BoardGameContext db,
            IUserService userService,
            IOptions<AppSettings> appSettings) : base(db)
        {
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]CreatePlayerDto playerDto)
        {
            var user = userService.Authenticate(playerDto.Username, playerDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new AuthenticatedPlayerDto(user, tokenString));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]CreatePlayerDto playerDto)
        {

            try
            {
                var player = playerDto.ToModel();
                this.
                // save 
                userService.Create(player, playerDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = this.db.Players
                .Include(p => p.GamePlaySessions)
                .Include(p => p.Ratings).Select(p => new PlayerStatsDto(p));
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = this.db.Players.First(p => p.Id == this.UserId);

            var player = this.db.Players
                .Include(p => p.GamePlaySessions)
                .ThenInclude(s => s.GamePlaySession)
                .ThenInclude(g => g.Game)
                .Include(p => p.GamePlaySessions)
                .ThenInclude(s => s.GamePlaySession)
                .ThenInclude(g => g.Winners)
                .Include(p => p.Ratings)
                .ThenInclude(g => g.Game)
                .Include(p => p.GamePlayWins)
                .SingleOrDefault(p => p.Id == id);

            return Ok(new PlayerDetailDto(player, user.CurrentYearFilter.Year));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CreatePlayerDto playerDto)
        {
            var player = playerDto.ToModel();
            player.Id = id;
            try
            {
                // save 
                userService.Update(player, playerDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}
