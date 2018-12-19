using BggApi.Dto;
using System.Threading.Tasks;

namespace BggApi.Service
{
    public interface IBggService
    {
        Task<BoardgameSearchResult> Search(string search, bool exact);

        Task<Boardgame> Get(int objectId);
    }
}
