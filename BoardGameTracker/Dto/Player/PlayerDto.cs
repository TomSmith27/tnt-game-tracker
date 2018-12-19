namespace BoardGameTracker.Dto{
    using BoardGameTracker.Models;
    public class PlayerDto
    {
        public PlayerDto(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.Username = player.Username;
        }
        public int Id { get; }
        public string Name { get; }
        public string Username { get; }
        
    }
}
