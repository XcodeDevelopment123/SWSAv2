using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanyofficialname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropTable(
                name: "CompanyOfficalContacts");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CompanyDepartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CompanyDepartmentId",
                table: "CompanyCommunicationContacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyOfficialContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOfficialContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOfficialContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommunicationContacts_CompanyDepartmentId",
                table: "CompanyCommunicationContacts",
                column: "CompanyDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOfficialContacts_CompanyId",
                table: "CompanyOfficialContacts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCommunicationContacts_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyCommunicationContacts",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCommunicationContacts_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropTable(
                name: "CompanyOfficialContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCommunicationContacts_CompanyDepartmentId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CompanyDepartments");

            migrationBuilder.DropColumn(
                name: "CompanyDepartmentId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.CreateTable(
                name: "CompanyOfficalContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOfficalContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOfficalContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOfficalContacts_CompanyId",
                table: "CompanyOfficalContacts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
