using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorcompanystatuslevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyActivityLevel",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyStatus",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CompanyStrikeOffSubmissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CompanyStrikeOffSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyActivityLevel",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyStatus",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuditSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LLPSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLPSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLPSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditSubmissions_WorkAssignmentId",
                table: "AuditSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LLPSubmissions_WorkAssignmentId",
                table: "LLPSubmissions",
                column: "WorkAssignmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditSubmissions");

            migrationBuilder.DropTable(
                name: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "CompanyActivityLevel",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyStatus",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyActivityLevel",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyStatus",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
