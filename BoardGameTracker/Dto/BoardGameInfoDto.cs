using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class BoardGameInfoDto
    {
        public BoardGameInfoDto(BoardGameEntry boardGame)
        {
            this.Id = boardGame.Id;
            this.Name = boardGame.Name;
            this.Description = boardGame.Description;
            this.Thumbnail = boardGame.Thumbnail;
            this.Image = boardGame.Image;
            this.ObjectId = boardGame.ObjectId;
          
        }
        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int ObjectId { get; }

        public string Thumbnail { get; }
        public string Image { get; }

    }
}
