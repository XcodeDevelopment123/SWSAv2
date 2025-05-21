using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorcompanyworkassignmentanduserrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companies");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountSubmitDate",
                table: "CompanyWorkProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnnualReturnSubmittedDate",
                table: "CompanyWorkProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBacklog",
                table: "CompanyWorkProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsJobCompleted",
                table: "CompanyWorkProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AGMDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ARDueDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedAuditUserId",
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

            migrationBuilder.AddColumn<int>(
                name: "CompanyStatus",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsYearEndActionRequired",
                table: "CompanyWorkAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkAssignmentUserMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentUserMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentUserMapping_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentUserMapping_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkAssignments_AssignedAuditUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedAuditUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMapping_UserId",
                table: "WorkAssignmentUserMapping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMapping_WorkAssignmentId",
                table: "WorkAssignmentUserMapping",
                column: "WorkAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedAuditUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedAuditUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropTable(
                name: "WorkAssignmentUserMapping");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWorkAssignments_AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AccountSubmitDate",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "AnnualReturnSubmittedDate",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "IsBacklog",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "IsJobCompleted",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "AGMDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "ARDueDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AssignedAuditUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "AuditMonthToDo",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyActivityType",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyStatus",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "IsYearEndActionRequired",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
