using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class fix_fk_nationalteam_player : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_NationalTeamId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NationalTeamId",
                table: "Players",
                column: "NationalTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_NationalTeamId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NationalTeamId",
                table: "Players",
                column: "NationalTeamId",
                unique: true);
        }
    }
}
