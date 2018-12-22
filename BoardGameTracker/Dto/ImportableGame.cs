namespace BoardGame.Api.Dto
{
    using System.Linq;
    using BggApi.Dto;
    using BoardGameTracker.Models;

    public class ImportableGame
    {
        public ImportableGame(BoardGameEntry boardGameEntry) : this(boardGameEntry.ObjectId, boardGameEntry.Name, boardGameEntry.YearPublished.Year, true)
        {
            
        }
        public ImportableGame(Boardgame boardGame) : this(boardGame.Objectid, boardGame.Name.SingleOrDefault(n => n.Primary)?.Text, boardGame.Yearpublished, false)
        {

        }

        public ImportableGame(int objectId, string name, int yearPublished, bool imported)
        {
            ObjectId = objectId;
            Name = name;
            YearPublished = yearPublished;
            Imported = imported;
        }

        

        public int ObjectId { get; }

        public string Name { get; }

        public int YearPublished { get; }

        public bool Imported { get; }
    }
}
