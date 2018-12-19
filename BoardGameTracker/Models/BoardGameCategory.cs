namespace BoardGameTracker.Models
{
    using System.Collections.Generic;

    public class BoardGameCategory
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public string Name { get; set; }

        public List<GameCategoryGameEntry> Games { get; set; }
    }
}
