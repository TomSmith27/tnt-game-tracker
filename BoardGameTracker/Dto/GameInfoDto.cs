namespace BoardGameTracker.Dto
{
    public class GameInfoDto
    {
        public GameInfoDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
    }
}