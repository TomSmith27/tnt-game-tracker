using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Models
{
    public class PlayerFriend
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public Player Friend { get; set; }
        public int FriendId { get; set; }
    }
}
