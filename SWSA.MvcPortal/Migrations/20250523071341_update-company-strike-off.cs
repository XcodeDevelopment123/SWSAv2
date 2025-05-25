using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanystrikeoff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "StrikeOffRemarks",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.AddColumn<string>(
                name: "StrikeOffRemarks",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId");
        }
    }
}
