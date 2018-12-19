namespace BoardGameTracker.Dto
{
    using BoardGameTracker.Models;

    public class AuthenticatedPlayerDto : PlayerDto
    {
        public AuthenticatedPlayerDto(Player player, string token) : base(player)
        {
            this.Token = token;
        }

        public string Token { get; }
    }
}
