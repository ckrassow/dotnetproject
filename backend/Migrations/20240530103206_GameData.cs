using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class GameData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamInvite_Teams_SenderId",
                table: "TeamInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<NationalTeamPrediction>_Teams_TeamId",
                table: "TeamPrediction<NationalTeamPrediction>");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<PlayerPrediction>_Teams_TeamId",
                table: "TeamPrediction<PlayerPrediction>");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<TournamentPrediction>_Teams_TeamId",
                table: "TeamPrediction<TournamentPrediction>");

            migrationBuilder.AddColumn<string>(
                name: "Crest",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tla",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Emblem = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Home = table.Column<int>(type: "integer", nullable: true),
                    Away = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PredictionTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Winner = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    FullTimeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_FullTimes_FullTimeId",
                        column: x => x.FullTimeId,
                        principalTable: "FullTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Matchday = table.Column<int>(type: "integer", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    Group = table.Column<string>(type: "text", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HomeTeamId = table.Column<int>(type: "integer", nullable: false),
                    AwayTeamId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionId = table.Column<int>(type: "integer", nullable: false),
                    ScoreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Scores_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Scores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CompetitionId",
                table: "Games",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ScoreId",
                table: "Games",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_FullTimeId",
                table: "Scores",
                column: "FullTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_PredictionTeams_TeamId",
                table: "Members",
                column: "TeamId",
                principalTable: "PredictionTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamInvite_PredictionTeams_SenderId",
                table: "TeamInvite",
                column: "SenderId",
                principalTable: "PredictionTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<NationalTeamPrediction>_PredictionTeams_Team~",
                table: "TeamPrediction<NationalTeamPrediction>",
                column: "TeamId",
                principalTable: "PredictionTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<PlayerPrediction>_PredictionTeams_TeamId",
                table: "TeamPrediction<PlayerPrediction>",
                column: "TeamId",
                principalTable: "PredictionTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<TournamentPrediction>_PredictionTeams_TeamId",
                table: "TeamPrediction<TournamentPrediction>",
                column: "TeamId",
                principalTable: "PredictionTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_PredictionTeams_TeamId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamInvite_PredictionTeams_SenderId",
                table: "TeamInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<NationalTeamPrediction>_PredictionTeams_Team~",
                table: "TeamPrediction<NationalTeamPrediction>");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<PlayerPrediction>_PredictionTeams_TeamId",
                table: "TeamPrediction<PlayerPrediction>");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPrediction<TournamentPrediction>_PredictionTeams_TeamId",
                table: "TeamPrediction<TournamentPrediction>");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PredictionTeams");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "FullTimes");

            migrationBuilder.DropColumn(
                name: "Crest",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Tla",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamInvite_Teams_SenderId",
                table: "TeamInvite",
                column: "SenderId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<NationalTeamPrediction>_Teams_TeamId",
                table: "TeamPrediction<NationalTeamPrediction>",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<PlayerPrediction>_Teams_TeamId",
                table: "TeamPrediction<PlayerPrediction>",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPrediction<TournamentPrediction>_Teams_TeamId",
                table: "TeamPrediction<TournamentPrediction>",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
