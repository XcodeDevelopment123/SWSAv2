using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class InvalidColumnsD5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuditExemption",
                table: "S14B",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "S14B",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditExemption",
                table: "S14B");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "S14B");

            migrationBuilder.RenameTable(
                name: "Referrals",
                schema: "dbo",
                newName: "Referrals");

            migrationBuilder.RenameTable(
                name: "Groups",
                schema: "dbo",
                newName: "Groups");
        }
    }
}
