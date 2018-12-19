using BggApi.Dto;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BggApi.Service
{
    public class BggService : IBggService
    {
        private readonly HttpClient client;

        public BggService()
        {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.boardgamegeek.com/xmlapi/"),
            };
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<Boardgame> Get(int objectId)
        {
            var response = await this.client.GetAsync($"boardgame/{objectId}?&stats=1");
            if (response.IsSuccessStatusCode)
            {

                using (var stream = new MemoryStream(await response.Content.ReadAsByteArrayAsync()))
                {
                    var serializer = new XmlSerializer(typeof(BoardGames));
                    var boardGames = (BoardGames)serializer.Deserialize(stream);
                    return boardGames.Boardgame;
                }
            }
            else
            {
                throw new Exception($"Unable to get Board game with id {objectId}");
            }
        }

        public async Task<BoardgameSearchResult> Search(string search, bool exact)
        {
            var result = await this.client.GetAsync($"search?search={search}&exact={(exact ? 1 : 0)}");
            if (result.IsSuccessStatusCode)
            {
                using (var stream = new MemoryStream(await result.Content.ReadAsByteArrayAsync()))
                {
                    var serializer = new XmlSerializer(typeof(BoardgameSearchResult));
                    var boardGames = (BoardgameSearchResult)serializer.Deserialize(stream);
                    return boardGames;
                }
            }
            else
            {
                throw new Exception("Unable to fetch search results");
            }
        }

    }
}
