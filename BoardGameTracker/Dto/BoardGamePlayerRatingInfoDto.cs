using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class BoardGamePlayerRatingInfoDto : BoardGameInfoDto
    {
        public BoardGamePlayerRatingInfoDto(BoardGameEntry boardGame, int rating) : base(boardGame)
        {
            this.Rating = rating;
        }

        public int Rating { get; }
    }
}
