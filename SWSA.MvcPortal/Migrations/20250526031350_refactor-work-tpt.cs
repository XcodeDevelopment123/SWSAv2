using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorworktpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "AGMDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "ARDueDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "SSMExtensionDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddColumn<int>(
                name: "WorkAssignmentId",
                table: "DocumentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_WorkAssignmentId",
                table: "DocumentRecords",
                column: "WorkAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStrikeOffSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_CompanyWorkAssignments_WorkAssignmentId",
                table: "DocumentRecords",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStrikeOffSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_CompanyWorkAssignments_WorkAssignmentId",
                table: "DocumentRecords");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRecords_WorkAssignmentId",
                table: "DocumentRecords");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStrikeOffSubmissions_WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "WorkAssignmentId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SSMExtensionDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
