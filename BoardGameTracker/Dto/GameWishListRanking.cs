using System.Collections.Generic;
using System.Linq;
using BoardGameTracker.Models;

namespace BoardGameTracker.Dto
{
    public class GameWishListRanking
    {
        public GameWishListRanking(IGrouping<BoardGameEntry, WishListEntry> wishListEntry)
        {
            this.Game = new GameInfoDto(wishListEntry.Key.Id, wishListEntry.Key.Name, wishListEntry.Key.Thumbnail);
            this.Players = wishListEntry.Select(w => new PlayerInfoDto(w.PlayerId, w.Player.Name, w.Player.Colour)).ToList();
        }

        public GameInfoDto Game { get; }

        public List<PlayerInfoDto> Players { get; set; }
    }
}
