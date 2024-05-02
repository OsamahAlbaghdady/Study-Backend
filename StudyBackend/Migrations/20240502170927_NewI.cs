using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndStructuer.Migrations
{
    /// <inheritdoc />
    public partial class NewI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Fields");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "DegreeFields",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "DegreeFields",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "DegreeFields");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "DegreeFields");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Fields",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Fields",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
