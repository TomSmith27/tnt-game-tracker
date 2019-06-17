using BoardGameTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Dto
{
    public class ActivityDto
    {
        public ActivityDto(Activity activity)
        {
            this.Id = activity.Id;
            this.Message = activity.Message;
            this.Date = activity.Date;
            this.Player = new PlayerDto(activity.Player);
        }
        public int Id { get;  }
        public string Message { get;  }
        public DateTimeOffset Date { get;  }
        public PlayerDto Player { get;  }
    }
}
