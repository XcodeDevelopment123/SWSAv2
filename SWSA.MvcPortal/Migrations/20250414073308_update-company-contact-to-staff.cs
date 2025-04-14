using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanycontacttostaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropTable(
                name: "CompanyStaffs");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "CompanyCommunicationContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLoginEnabled",
                table: "CompanyCommunicationContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "CompanyCommunicationContacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "CompanyCommunicationContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "CompanyCommunicationContacts",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommunicationContacts_StaffId",
                table: "CompanyCommunicationContacts",
                column: "StaffId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyCommunicationContacts_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "CompanyCommunicationContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyCommunicationContacts_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCommunicationContacts_StaffId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "IsLoginEnabled",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "CompanyCommunicationContacts");

            migrationBuilder.CreateTable(
                name: "CompanyStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false, computedColumnSql: "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)", stored: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_CompanyStaffs_CompanyDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "CompanyDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_CompanyId",
                table: "CompanyStaffs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_DepartmentId",
                table: "CompanyStaffs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_StaffId",
                table: "CompanyStaffs",
                column: "StaffId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id");
        }
    }
}
