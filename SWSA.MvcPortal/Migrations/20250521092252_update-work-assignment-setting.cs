using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateworkassignmentsetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWorkAssignments_AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWorkAssignments_AssignedUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AuditMonthToDo",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyActivityType",
                table: "CompanyWorkAssignments");

            migrationBuilder.RenameColumn(
                name: "IsYearEndActionRequired",
                table: "CompanyWorkAssignments",
                newName: "IsYearEndTask");

            migrationBuilder.AddColumn<DateTime>(
                name: "SSMExtensionDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAccountingMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentAccountingMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentAccountingMonth_CompanyWorkAssignments_CompanyWorkAssignmentId",
                        column: x => x.CompanyWorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAuditMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentAuditMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentAuditMonth_CompanyWorkAssignments_CompanyWorkAssignmentId",
                        column: x => x.CompanyWorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAccountingMonth_CompanyWorkAssignmentId",
                table: "WorkAssignmentAccountingMonth",
                column: "CompanyWorkAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAuditMonth_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonth",
                column: "CompanyWorkAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAssignmentAccountingMonth");

            migrationBuilder.DropTable(
                name: "WorkAssignmentAuditMonth");

            migrationBuilder.DropColumn(
                name: "SSMExtensionDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.RenameColumn(
                name: "IsYearEndTask",
                table: "CompanyWorkAssignments",
                newName: "IsYearEndActionRequired");

            migrationBuilder.AddColumn<int>(
                name: "AssignedAuditUserId",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuditMonthToDo",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyActivityType",
                table: "CompanyWorkAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkAssignments_AssignedAuditUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedAuditUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkAssignments_AssignedUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedAuditUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedAuditUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
