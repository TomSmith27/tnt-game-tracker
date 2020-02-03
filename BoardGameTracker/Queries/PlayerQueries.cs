using BoardGameTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameTracker.Queries
{
    public static class PlayerQueries
    {
        public static IQueryable<Player> FilterFriends(this IQueryable<Player> players, int userId)
        {
            return players.Where(p => p.Followers.Any(f => f.PlayerId == userId) || p.Id == userId);
        }
    }
}
