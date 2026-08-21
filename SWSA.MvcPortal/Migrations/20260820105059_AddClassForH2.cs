using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddClassForH2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientDateReceived",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientDateSent",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateEnd",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateStart",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOrYDays",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Staff",
                table: "TX4",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientDateReceived",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "ClientDateSent",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "NOrYDays",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "TX4");

            migrationBuilder.DropColumn(
                name: "Staff",
                table: "TX4");
        }
    }
}
