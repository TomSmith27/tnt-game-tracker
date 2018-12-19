using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameTracker.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ObjectId = table.Column<int>(nullable: false),
                    YearPublished = table.Column<DateTimeOffset>(nullable: false),
                    MinPlayers = table.Column<int>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    PlayingTime = table.Column<int>(nullable: false),
                    MinPlaytime = table.Column<int>(nullable: false),
                    MaxPlaytime = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    AverageRating = table.Column<double>(nullable: false),
                    UsersRated = table.Column<int>(nullable: false),
                    BestPlayerCount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameCategoryGameEntry",
                columns: table => new
                {
                    BoardGameEntryId = table.Column<int>(nullable: false),
                    BoardGameCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategoryGameEntry", x => new { x.BoardGameCategoryId, x.BoardGameEntryId });
                    table.ForeignKey(
                        name: "FK_GameCategoryGameEntry_Categories_BoardGameCategoryId",
                        column: x => x.BoardGameCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCategoryGameEntry_Games_BoardGameEntryId",
                        column: x => x.BoardGameEntryId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlaySessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlaySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlaySessions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_Ratings_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlaySessionPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    GamePlaySessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlaySessionPlayer", x => new { x.GamePlaySessionId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_GamePlaySessionPlayer_GamePlaySessions_GamePlaySessionId",
                        column: x => x.GamePlaySessionId,
                        principalTable: "GamePlaySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlaySessionPlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GamePlaySessions",
                columns: new[] { "Id", "Date", "GameId" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2018, 12, 19, 21, 45, 49, 401, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Age", "AverageRating", "BestPlayerCount", "Description", "Image", "MaxPlayers", "MaxPlaytime", "MinPlayers", "MinPlaytime", "Name", "ObjectId", "PlayingTime", "Thumbnail", "UsersRated", "YearPublished" },
                values: new object[] { 1, 0, 0.0, null, null, null, 0, 0, 0, 0, "Azul", 0, 0, null, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, "Admin", new byte[] { 56, 188, 29, 154, 139, 68, 170, 219, 75, 179, 108, 164, 165, 12, 78, 93, 237, 213, 240, 240, 129, 1, 195, 245, 182, 78, 241, 34, 127, 142, 240, 181, 68, 18, 43, 154, 144, 123, 18, 55, 48, 180, 16, 159, 14, 37, 179, 5, 216, 255, 75, 246, 182, 214, 215, 213, 65, 108, 112, 21, 0, 140, 129, 221 }, new byte[] { 118, 114, 33, 38, 250, 92, 241, 72, 89, 49, 191, 255, 168, 152, 167, 10, 233, 203, 156, 8, 41, 176, 21, 103, 147, 176, 82, 160, 195, 208, 68, 31, 119, 66, 66, 226, 163, 148, 216, 146, 238, 197, 190, 13, 104, 173, 172, 130, 124, 175, 99, 165, 254, 192, 195, 83, 118, 7, 124, 121, 140, 217, 20, 45, 243, 43, 172, 28, 13, 217, 141, 187, 168, 46, 221, 250, 146, 190, 92, 27, 17, 175, 51, 119, 104, 72, 253, 121, 119, 211, 123, 71, 241, 168, 208, 59, 43, 204, 116, 35, 116, 100, 174, 7, 31, 255, 148, 166, 18, 198, 142, 78, 128, 71, 144, 14, 24, 105, 37, 203, 40, 54, 50, 11, 135, 198, 174, 109 }, "admin" });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "GameId", "PlayerId", "Rating" },
                values: new object[] { 1, 1, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_GameCategoryGameEntry_BoardGameEntryId",
                table: "GameCategoryGameEntry",
                column: "BoardGameEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlaySessionPlayer_PlayerId",
                table: "GamePlaySessionPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlaySessions_GameId",
                table: "GamePlaySessions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PlayerId",
                table: "Ratings",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCategoryGameEntry");

            migrationBuilder.DropTable(
                name: "GamePlaySessionPlayer");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "GamePlaySessions");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
