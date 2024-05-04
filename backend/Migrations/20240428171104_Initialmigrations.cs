using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NationalTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PlayoffAppearences = table.Column<int>(type: "integer", nullable: false),
                    FifaRanking = table.Column<int>(type: "integer", nullable: false),
                    Group = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<int>(type: "integer", nullable: false),
                    Pos = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Caps = table.Column<int>(type: "integer", nullable: false),
                    Goals = table.Column<int>(type: "integer", nullable: false),
                    Club = table.Column<string>(type: "text", nullable: true),
                    NationalTeamId = table.Column<int>(type: "integer", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_NationalTeams_NationalTeamId",
                        column: x => x.NationalTeamId,
                        principalTable: "NationalTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredictionType = table.Column<int>(type: "integer", nullable: false),
                    NationalTeamId = table.Column<int>(type: "integer", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPredictions_NationalTeams_NationalTeamId",
                        column: x => x.NationalTeamId,
                        principalTable: "NationalTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamPredictions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TournamentPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredictionType = table.Column<int>(type: "integer", nullable: false),
                    PredictionValue = table.Column<string>(type: "text", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentPredictions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    NationalTeamId = table.Column<int>(type: "integer", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_NationalTeams_NationalTeamId",
                        column: x => x.NationalTeamId,
                        principalTable: "NationalTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredictionType = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerPredictions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerPredictions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTeamPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeamPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTeamPredictions_TeamPredictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "TeamPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamPredictions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTournamentPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTournamentPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTournamentPredictions_TournamentPredictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "TournamentPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTournamentPredictions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPlayerPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayerPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPlayerPredictions_PlayerPredictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "PlayerPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayerPredictions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPredictions_PlayerId",
                table: "PlayerPredictions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPredictions_TeamId",
                table: "PlayerPredictions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NationalTeamId",
                table: "Players",
                column: "NationalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPredictions_NationalTeamId",
                table: "TeamPredictions",
                column: "NationalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPredictions_TeamId",
                table: "TeamPredictions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPredictions_TeamId",
                table: "TournamentPredictions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayerPredictions_PredictionId",
                table: "UserPlayerPredictions",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayerPredictions_UserId",
                table: "UserPlayerPredictions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalTeamId",
                table: "Users",
                column: "NationalTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamPredictions_PredictionId",
                table: "UserTeamPredictions",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamPredictions_UserId",
                table: "UserTeamPredictions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTournamentPredictions_PredictionId",
                table: "UserTournamentPredictions",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTournamentPredictions_UserId",
                table: "UserTournamentPredictions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPlayerPredictions");

            migrationBuilder.DropTable(
                name: "UserTeamPredictions");

            migrationBuilder.DropTable(
                name: "UserTournamentPredictions");

            migrationBuilder.DropTable(
                name: "PlayerPredictions");

            migrationBuilder.DropTable(
                name: "TeamPredictions");

            migrationBuilder.DropTable(
                name: "TournamentPredictions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "NationalTeams");
        }
    }
}
