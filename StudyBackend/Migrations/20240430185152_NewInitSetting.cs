using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndStructuer.Migrations
{
    /// <inheritdoc />
    public partial class NewInitSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "WelcomeVideoUrl");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Settings",
                newName: "WelcomeMessage");

            migrationBuilder.AddColumn<string>(
                name: "ContactTelegram",
                table: "Settings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactWhatsApp",
                table: "Settings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactTelegram",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ContactWhatsApp",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "WelcomeVideoUrl",
                table: "Settings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "WelcomeMessage",
                table: "Settings",
                newName: "Key");
        }
    }
}
