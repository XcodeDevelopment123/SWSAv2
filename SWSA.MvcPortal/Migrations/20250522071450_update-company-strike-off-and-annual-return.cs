using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanystrikeoffandannualreturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlowType",
                table: "DocumentRecords",
                newName: "DocumentFlow");

            migrationBuilder.AddColumn<bool>(
                name: "IsStrikedOff",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StrikeOffEffectiveDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrikeOffRemarks",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StrikeOffStatus",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnnualReturnSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedARDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfAnnualReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualReturnSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualReturnSubmissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStrikeOffSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IRBSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMStrikeOffDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStrikeOffSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStrikeOffSubmissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualReturnSubmissions_CompanyId",
                table: "AnnualReturnSubmissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualReturnSubmissions_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualReturnSubmissions");

            migrationBuilder.DropTable(
                name: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "IsStrikedOff",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrikeOffEffectiveDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrikeOffRemarks",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrikeOffStatus",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "DocumentFlow",
                table: "DocumentRecords",
                newName: "FlowType");
        }
    }
}
