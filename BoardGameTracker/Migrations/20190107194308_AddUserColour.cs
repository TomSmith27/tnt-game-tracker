using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGameTracker.Migrations
{
    public partial class AddUserColour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlaySessions_Games_GameId",
                table: "GamePlaySessions");


            migrationBuilder.AddColumn<int>(
                name: "colour",
                table: "Players",
                nullable: false,
                defaultValue: 4209210);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GamePlaySessions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlaySessions_Games_GameId",
                table: "GamePlaySessions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlaySessions_Games_GameId",
                table: "GamePlaySessions");

            migrationBuilder.DropColumn(
                name: "colour",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GamePlaySessions",
                nullable: true,
                oldClrType: typeof(int));


            migrationBuilder.AddForeignKey(
                name: "FK_GamePlaySessions_Games_GameId",
                table: "GamePlaySessions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
