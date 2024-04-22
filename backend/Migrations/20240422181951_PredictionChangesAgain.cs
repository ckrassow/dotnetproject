using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class PredictionChangesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPredictions_Users_UserId",
                table: "PlayerPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPredictions_Users_UserId",
                table: "TeamPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPredictions_Users_UserId",
                table: "TournamentPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TournamentPredictions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TeamPredictions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlayerPredictions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPredictions_Users_UserId",
                table: "PlayerPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPredictions_Users_UserId",
                table: "TeamPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPredictions_Users_UserId",
                table: "TournamentPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPredictions_Users_UserId",
                table: "PlayerPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPredictions_Users_UserId",
                table: "TeamPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPredictions_Users_UserId",
                table: "TournamentPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TournamentPredictions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TeamPredictions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlayerPredictions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPredictions_Users_UserId",
                table: "PlayerPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPredictions_Users_UserId",
                table: "TeamPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPredictions_Users_UserId",
                table: "TournamentPredictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
