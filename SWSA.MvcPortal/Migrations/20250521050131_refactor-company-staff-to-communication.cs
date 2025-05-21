using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorcompanystafftocommunication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_CompanyStaffId",
                table: "SystemAuditLogs");

            migrationBuilder.DropTable(
                name: "CompanyStaffs");

            migrationBuilder.DropIndex(
                name: "IX_SystemAuditLogs_CompanyStaffId",
                table: "SystemAuditLogs");

            migrationBuilder.DropColumn(
                name: "CompanyStaffId",
                table: "SystemAuditLogs");

            migrationBuilder.DropColumn(
                name: "SSMExtensionDate",
                table: "CompanyComplianceDates");

            migrationBuilder.CreateTable(
                name: "CompanyCommunicationContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCommunicationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCommunicationContact_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommunicationContact_CompanyId",
                table: "CompanyCommunicationContact",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyCommunicationContact");

            migrationBuilder.AddColumn<int>(
                name: "CompanyStaffId",
                table: "SystemAuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SSMExtensionDate",
                table: "CompanyComplianceDates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false, computedColumnSql: "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)", stored: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStaffs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemAuditLogs_CompanyStaffId",
                table: "SystemAuditLogs",
                column: "CompanyStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_CompanyId",
                table: "CompanyStaffs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_StaffId",
                table: "CompanyStaffs",
                column: "StaffId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_CompanyStaffs_CompanyStaffId",
                table: "SystemAuditLogs",
                column: "CompanyStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id");
        }
    }
}
