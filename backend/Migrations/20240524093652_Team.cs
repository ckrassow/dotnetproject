using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class Team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPredictions_Teams_TeamId",
                table: "PlayerPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPredictions_Teams_TeamId",
                table: "TeamPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentPredictions_Teams_TeamId",
                table: "TournamentPredictions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_TeamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TournamentPredictions_TeamId",
                table: "TournamentPredictions");

            migrationBuilder.DropIndex(
                name: "IX_TeamPredictions_TeamId",
                table: "TeamPredictions");

            migrationBuilder.DropIndex(
                name: "IX_PlayerPredictions_TeamId",
                table: "PlayerPredictions");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TournamentPredictions");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "TeamPredictions");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PlayerPredictions");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamMemberId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    IsCaptain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Users_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    Accepted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamInvite_Teams_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamInvite_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPrediction<NationalTeamPrediction>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPrediction<NationalTeamPrediction>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<NationalTeamPrediction>_TeamPredictions_Pred~",
                        column: x => x.PredictionId,
                        principalTable: "TeamPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<NationalTeamPrediction>_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPrediction<PlayerPrediction>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPrediction<PlayerPrediction>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<PlayerPrediction>_PlayerPredictions_Predicti~",
                        column: x => x.PredictionId,
                        principalTable: "PlayerPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<PlayerPrediction>_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPrediction<TournamentPrediction>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    PredictionId = table.Column<int>(type: "integer", nullable: false),
                    PredictionTypeString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPrediction<TournamentPrediction>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<TournamentPrediction>_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPrediction<TournamentPrediction>_TournamentPredictions_~",
                        column: x => x.PredictionId,
                        principalTable: "TournamentPredictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_TeamId",
                table: "Members",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_TeamMemberId",
                table: "Members",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvite_RecipientId",
                table: "TeamInvite",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamInvite_SenderId",
                table: "TeamInvite",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<NationalTeamPrediction>_PredictionId",
                table: "TeamPrediction<NationalTeamPrediction>",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<NationalTeamPrediction>_TeamId",
                table: "TeamPrediction<NationalTeamPrediction>",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<PlayerPrediction>_PredictionId",
                table: "TeamPrediction<PlayerPrediction>",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<PlayerPrediction>_TeamId",
                table: "TeamPrediction<PlayerPrediction>",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<TournamentPrediction>_PredictionId",
                table: "TeamPrediction<TournamentPrediction>",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPrediction<TournamentPrediction>_TeamId",
                table: "TeamPrediction<TournamentPrediction>",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "TeamInvite");

            migrationBuilder.DropTable(
                name: "TeamPrediction<NationalTeamPrediction>");

            migrationBuilder.DropTable(
                name: "TeamPrediction<PlayerPrediction>");

            migrationBuilder.DropTable(
                name: "TeamPrediction<TournamentPrediction>");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TournamentPredictions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "TeamPredictions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PlayerPredictions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPredictions_TeamId",
                table: "TournamentPredictions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPredictions_TeamId",
                table: "TeamPredictions",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPredictions_TeamId",
                table: "PlayerPredictions",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPredictions_Teams_TeamId",
                table: "PlayerPredictions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPredictions_Teams_TeamId",
                table: "TeamPredictions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentPredictions_Teams_TeamId",
                table: "TournamentPredictions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_TeamId",
                table: "Users",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
