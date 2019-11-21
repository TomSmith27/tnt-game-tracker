namespace BoardGame.Api.Dto
{
    using System.Linq;
    using BggApi.Dto;
    using BoardGameTracker.Models;

    public class ImportableGame
    {
        public ImportableGame(BoardGameEntry boardGameEntry) : this(boardGameEntry.ObjectId, boardGameEntry.Name, boardGameEntry.YearPublished.Year, true, boardGameEntry.Id)
        {
            
        }
        public ImportableGame(Boardgame boardGame) : this(boardGame.Objectid, boardGame.Name.SingleOrDefault(n => n.Primary)?.Text, boardGame.Yearpublished, false, null)
        {

        }

        public ImportableGame(int objectId, string name, int yearPublished, bool imported, int? id)
        {
            ObjectId = objectId;
            Name = name;
            YearPublished = yearPublished;
            Imported = imported;
            Id = id;
        }

        
        public int? Id { get; }
        public int ObjectId { get; }

        public string Name { get; }

        public int YearPublished { get; }

        public bool Imported { get; }
    }
}
