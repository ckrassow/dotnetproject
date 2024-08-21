using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class GamePredictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Matchday",
                table: "Games",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "Games",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "GamePredictions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    PredictedHomeScore = table.Column<int>(type: "integer", nullable: true),
                    PredictedAwayScore = table.Column<int>(type: "integer", nullable: true),
                    UtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePredictions", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_GamePredictions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePredictions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePredictions_GameId",
                table: "GamePredictions",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePredictions");

            migrationBuilder.AlterColumn<int>(
                name: "Matchday",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
