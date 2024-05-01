using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndStructuer.Migrations
{
    /// <inheritdoc />
    public partial class OldInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Fields");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "DegreeFields",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WelcomeMessage = table.Column<string>(type: "text", nullable: true),
                    WelcomeVideoUrl = table.Column<string>(type: "text", nullable: true),
                    ContactWhatsApp = table.Column<string>(type: "text", nullable: true),
                    ContactTelegram = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DegreeFields");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Fields",
                type: "bigint",
                nullable: true);
        }
    }
}
