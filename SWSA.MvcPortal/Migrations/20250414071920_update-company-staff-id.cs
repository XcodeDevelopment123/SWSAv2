using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanystaffid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "CompanyStaffs",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_StaffId",
                table: "CompanyStaffs",
                column: "StaffId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyStaffs_StaffId",
                table: "CompanyStaffs");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "CompanyStaffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComputedColumnSql: "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)");
        }
    }
}
