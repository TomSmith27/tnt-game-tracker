namespace BoardGameTracker.Dto
{
    using BoardGameTracker.Models;

    public class PlayerSessionDto : PlayerDto
    {
        public PlayerSessionDto(Player player, bool isWinner) : base(player)
        {
            this.IsWinner = isWinner;
        }

        public bool IsWinner { get; }
    }
}
