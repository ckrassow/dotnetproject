using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroPredApi.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePicRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicRef",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicRef",
                table: "Users");
        }
    }
}
