using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameTracker.Migrations
{
    public partial class AddWinners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlaySessionWinner",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    GamePlaySessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlaySessionWinner", x => new { x.GamePlaySessionId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_GamePlaySessionWinner_GamePlaySessions_GamePlaySessionId",
                        column: x => x.GamePlaySessionId,
                        principalTable: "GamePlaySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlaySessionWinner_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlaySessionWinner_PlayerId",
                table: "GamePlaySessionWinner",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlaySessionWinner");
        }
    }
}
