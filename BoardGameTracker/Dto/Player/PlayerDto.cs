namespace BoardGameTracker.Dto{
    using BoardGameTracker.Models;
    public class PlayerDto
    {
        public PlayerDto(Player player)
        {
            this.Id = player.Id;
            this.Name = player.Name;
            this.Username = player.Username;
            this.Colour = "#" + player.Colour.R.ToString("X2") + player.Colour.G.ToString("X2") + player.Colour.B.ToString("X2");
        }
        public int Id { get; }
        public string Name { get; }
        public string Username { get; }
        public string Colour { get; }
        
    }
}
