using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorcompanystafflogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_PerformedByCompanyStaffId",
                table: "SystemAuditLogs");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "CompanyStaffs");

            migrationBuilder.DropColumn(
                name: "IsLoginEnabled",
                table: "CompanyStaffs");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "CompanyStaffs");

            migrationBuilder.RenameColumn(
                name: "PerformedByCompanyStaffId",
                table: "SystemAuditLogs",
                newName: "CompanyStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemAuditLogs_PerformedByCompanyStaffId",
                table: "SystemAuditLogs",
                newName: "IX_SystemAuditLogs_CompanyStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_CompanyStaffId",
                table: "SystemAuditLogs",
                column: "CompanyStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_CompanyStaffId",
                table: "SystemAuditLogs");

            migrationBuilder.RenameColumn(
                name: "CompanyStaffId",
                table: "SystemAuditLogs",
                newName: "PerformedByCompanyStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemAuditLogs_CompanyStaffId",
                table: "SystemAuditLogs",
                newName: "IX_SystemAuditLogs_PerformedByCompanyStaffId");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "CompanyStaffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLoginEnabled",
                table: "CompanyStaffs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "CompanyStaffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_PerformedByCompanyStaffId",
                table: "SystemAuditLogs",
                column: "PerformedByCompanyStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
