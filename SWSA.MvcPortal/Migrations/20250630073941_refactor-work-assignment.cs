using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualReturnSubmissions");

            migrationBuilder.DropTable(
                name: "AuditSubmissions");

            migrationBuilder.DropTable(
                name: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropTable(
                name: "LLPSubmissions");

            migrationBuilder.DropTable(
                name: "WorkAssignmentMonths");

            migrationBuilder.DropColumn(
                name: "IsYearEndTask",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "IsStrikedOff",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrikeOffEffectiveDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StrikeOffStatus",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "WorkType",
                table: "CompanyWorkAssignments",
                newName: "ForYear");

            migrationBuilder.CreateTable(
                name: "AnnualReturnWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedARDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfAnnualReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualReturnWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualReturnWorkAssignments_CompanyWorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstYearAccountStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedCirculation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    ReasonForLate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditWorkAssignments_CompanyWorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LLPWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SSMExtensionDateForAcc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLPWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLPWorkAssignments_CompanyWorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrikeOffWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IRBSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMStrikeOffDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrikeOffWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrikeOffWorkAssignments_CompanyWorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualReturnWorkAssignments");

            migrationBuilder.DropTable(
                name: "AuditWorkAssignments");

            migrationBuilder.DropTable(
                name: "LLPWorkAssignments");

            migrationBuilder.DropTable(
                name: "StrikeOffWorkAssignments");

            migrationBuilder.RenameColumn(
                name: "ForYear",
                table: "CompanyWorkAssignments",
                newName: "WorkType");

            migrationBuilder.AddColumn<bool>(
                name: "IsYearEndTask",
                table: "CompanyWorkAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfAnnualReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedARDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualReturnSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    AccDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstYearAccountStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    ReasonForLate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedCirculation = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "CompanyStrikeOffSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    IRBSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSMStrikeOffDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_CompanyStrikeOffSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LLPSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSMExtensionDateForAcc = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "WorkAssignmentMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentMonths_CompanyWorkAssignments_CompanyWorkAssignmentId",
                        column: x => x.CompanyWorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualReturnSubmissions_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditSubmissions_WorkAssignmentId",
                table: "AuditSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_CompanyId",
                table: "CompanyStrikeOffSubmissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStrikeOffSubmissions_WorkAssignmentId",
                table: "CompanyStrikeOffSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LLPSubmissions_WorkAssignmentId",
                table: "LLPSubmissions",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentMonths",
                column: "CompanyWorkAssignmentId");
        }
    }
}
