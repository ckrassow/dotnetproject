using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class PredictionChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPredictions_Players_PlayerId",
                table: "PlayerPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPredictions_NationalTeams_NationalTeamId",
                table: "TeamPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "NationalTeamId",
                table: "TeamPredictions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "PlayerPredictions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPredictions_Players_PlayerId",
                table: "PlayerPredictions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPredictions_NationalTeams_NationalTeamId",
                table: "TeamPredictions",
                column: "NationalTeamId",
                principalTable: "NationalTeams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPredictions_Players_PlayerId",
                table: "PlayerPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPredictions_NationalTeams_NationalTeamId",
                table: "TeamPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "NationalTeamId",
                table: "TeamPredictions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "PlayerPredictions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPredictions_Players_PlayerId",
                table: "PlayerPredictions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPredictions_NationalTeams_NationalTeamId",
                table: "TeamPredictions",
                column: "NationalTeamId",
                principalTable: "NationalTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
