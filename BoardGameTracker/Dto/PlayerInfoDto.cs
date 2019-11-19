using System.Drawing;

namespace BoardGameTracker.Dto
{
    public class PlayerInfoDto
    {
        public PlayerInfoDto(int id, string name, Color colour)
        {
            Id = id;
            Name = name;
            this.Colour = "#" + colour.R.ToString("X2") + colour.G.ToString("X2") + colour.B.ToString("X2");
        }
        public int Id { get; }
        public string Name { get; }
        public string Colour { get; }
    }
}