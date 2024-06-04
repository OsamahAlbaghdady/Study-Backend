using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBackend.Migrations
{
    /// <inheritdoc />
    public partial class NewwVideoCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Degrees");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Countries",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Degrees",
                type: "text",
                nullable: true);
        }
    }
}
