namespace BoardGameTracker.Dto
{
    public class PlayerInfoDto
    {
        public PlayerInfoDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
    }
}