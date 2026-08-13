using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnsToAex12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuditExrmption",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsBinded",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KuchingReviewDays",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SigningFirm",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetTaxWorkDate",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalFieldWkDays",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhichDB",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YEnd",
                table: "AEX12",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditExrmption",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "IsBinded",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "KuchingReviewDays",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "SigningFirm",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "TargetTaxWorkDate",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "TotalFieldWkDays",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "WhichDB",
                table: "AEX12");

            migrationBuilder.DropColumn(
                name: "YEnd",
                table: "AEX12");
        }
    }
}
