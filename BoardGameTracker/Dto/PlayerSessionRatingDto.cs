using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class PlayerSessionRatingDto
    {
        public Player Player { get; }
        public int? Rating { get; }

        public PlayerSessionRatingDto(GamePlaySessionPlayer p, PlayerRating playerRating)
        {
            this.Player = p.Player;
            this.Rating = playerRating?.Rating;
        }
    }
}
