using System.Collections.Generic;
using System.Linq;
using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class GameWishListRanking
    {
        public GameWishListRanking(IGrouping<BoardGameEntry, WishListEntry> wishListEntry)
        {
            this.Game = new GameInfoDto(wishListEntry.Key.Id, wishListEntry.Key.Name);
            this.Players = wishListEntry.Select(w => new PlayerInfoDto(w.PlayerId, w.Player.Name)).ToList();
        }

        public GameInfoDto Game { get; }

        public List<PlayerInfoDto> Players { get; set; }
    }
}
