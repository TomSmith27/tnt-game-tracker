namespace BoardGameTracker.Dto
{
    public class GameInfoDto
    {
        public GameInfoDto(int id, string name, string thumbnail)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnail;
        }
        public int Id { get; }
        public string Name { get; }
        public string Thumbnail { get; }
    }
}