using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class BoardGamePlayerPlaysInfoDto : BoardGameInfoDto
    {
        public BoardGamePlayerPlaysInfoDto(BoardGameEntry boardGame, int plays) : base(boardGame)
        {
            this.Plays = plays;
        }

        public int Plays { get; }
    }
}
