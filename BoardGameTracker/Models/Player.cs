namespace BoardGameTracker.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<GamePlaySessionPlayer> GamePlaySessions { get; set; }
        public ICollection<PlayerRating> Ratings { get; set; }
 
        private int colour;

        [NotMapped]
        public Color Colour
        {
            get
            {
                return Color.FromArgb(this.colour);
            }
            set
            {
                this.colour = value.ToArgb();
            }
        }

    }
}