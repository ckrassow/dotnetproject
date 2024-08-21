using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class Gamechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "AwayTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Competitions");

            migrationBuilder.AlterColumn<string>(
                name: "Winner",
                table: "Scores",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "HalfTimeId",
                table: "Scores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AwayTeam",
                table: "Games",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeTeam",
                table: "Games",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HalfTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Home = table.Column<int>(type: "integer", nullable: true),
                    Away = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HalfTimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_HalfTimeId",
                table: "Scores",
                column: "HalfTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_HalfTimes_HalfTimeId",
                table: "Scores",
                column: "HalfTimeId",
                principalTable: "HalfTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_HalfTimes_HalfTimeId",
                table: "Scores");

            migrationBuilder.DropTable(
                name: "HalfTimes");

            migrationBuilder.DropIndex(
                name: "IX_Scores_HalfTimeId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "HalfTimeId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Winner",
                table: "Scores",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Scores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Competitions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Competitions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Crest = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Tla = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
