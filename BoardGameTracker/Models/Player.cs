using System;

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
        public bool IsAdmin { get; set; }
        public List<GamePlaySessionPlayer> GamePlaySessions { get; set; } = new List<GamePlaySessionPlayer>();
        public List<GamePlaySessionWinner> GamePlayWins { get; set; } = new List<GamePlaySessionWinner>();
        public ICollection<PlayerRating> Ratings { get; set; }
        public ICollection<WishListEntry> WishList { get; set; }
        public DateTime CurrentYearFilter { get; set; }

        public ICollection<PlayerFriend> Friends { get; set; } = new List<PlayerFriend>();
        public ICollection<PlayerFriend> Followers { get; set; } = new List<PlayerFriend>();
 
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