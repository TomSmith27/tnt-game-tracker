using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameTracker.Migrations
{
    public partial class addnotestogames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "GamePlaySessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "GamePlaySessions");
        }
    }
}
