using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
    }
}
