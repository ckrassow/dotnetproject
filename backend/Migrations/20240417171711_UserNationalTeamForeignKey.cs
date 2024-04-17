using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class UserNationalTeamForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouriteTeam",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "NationalTeamId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalTeamId",
                table: "Users",
                column: "NationalTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_NationalTeams_NationalTeamId",
                table: "Users",
                column: "NationalTeamId",
                principalTable: "NationalTeams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_NationalTeams_NationalTeamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_NationalTeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalTeamId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FavouriteTeam",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
