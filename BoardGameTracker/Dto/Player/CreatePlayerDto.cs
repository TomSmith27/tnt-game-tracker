namespace BoardGameTracker.Dto
{
    using Models;
    using System.Drawing;

    public class CreatePlayerDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Colour { get; set; }

        public Player ToModel()
        {
            return new Player()
            {
                Name =  this.Name,
                Username = this.Username,
                Colour = Color.FromArgb(this.Colour)
            };
        }
    }
}
