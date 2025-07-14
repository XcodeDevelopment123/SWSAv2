using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class renameworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnWorkAssignments_CompanyWorkAssignments_Id",
                table: "AnnualReturnWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditWorkAssignments_CompanyWorkAssignments_Id",
                table: "AuditWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Companies_CompanyId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_LLPWorkAssignments_CompanyWorkAssignments_Id",
                table: "LLPWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StrikeOffWorkAssignments_CompanyWorkAssignments_Id",
                table: "StrikeOffWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMappings_CompanyWorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropTable(
                name: "CompanyWorkProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyWorkAssignments",
                table: "CompanyWorkAssignments");

            migrationBuilder.RenameTable(
                name: "CompanyWorkAssignments",
                newName: "WorkAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyWorkAssignments_CompanyId",
                table: "WorkAssignments",
                newName: "IX_WorkAssignments_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssignments",
                table: "WorkAssignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WorkProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeTakenInDays = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProgressNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsJobCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsBacklog = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProgresses_WorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkProgresses_WorkAssignmentId",
                table: "WorkProgresses",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnWorkAssignments_WorkAssignments_Id",
                table: "AnnualReturnWorkAssignments",
                column: "Id",
                principalTable: "WorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditWorkAssignments_WorkAssignments_Id",
                table: "AuditWorkAssignments",
                column: "Id",
                principalTable: "WorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LLPWorkAssignments_WorkAssignments_Id",
                table: "LLPWorkAssignments",
                column: "Id",
                principalTable: "WorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StrikeOffWorkAssignments_WorkAssignments_Id",
                table: "StrikeOffWorkAssignments",
                column: "Id",
                principalTable: "WorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignments_Companies_CompanyId",
                table: "WorkAssignments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentUserMappings_WorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMappings",
                column: "WorkAssignmentId",
                principalTable: "WorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnWorkAssignments_WorkAssignments_Id",
                table: "AnnualReturnWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditWorkAssignments_WorkAssignments_Id",
                table: "AuditWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_LLPWorkAssignments_WorkAssignments_Id",
                table: "LLPWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StrikeOffWorkAssignments_WorkAssignments_Id",
                table: "StrikeOffWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignments_Companies_CompanyId",
                table: "WorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMappings_WorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropTable(
                name: "WorkProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssignments",
                table: "WorkAssignments");

            migrationBuilder.RenameTable(
                name: "WorkAssignments",
                newName: "CompanyWorkAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignments_CompanyId",
                table: "CompanyWorkAssignments",
                newName: "IX_CompanyWorkAssignments_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyWorkAssignments",
                table: "CompanyWorkAssignments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CompanyWorkProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBacklog = table.Column<bool>(type: "bit", nullable: false),
                    IsJobCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ProgressNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeTakenInDays = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWorkProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyWorkProgresses_CompanyWorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkProgresses_WorkAssignmentId",
                table: "CompanyWorkProgresses",
                column: "WorkAssignmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnWorkAssignments_CompanyWorkAssignments_Id",
                table: "AnnualReturnWorkAssignments",
                column: "Id",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditWorkAssignments_CompanyWorkAssignments_Id",
                table: "AuditWorkAssignments",
                column: "Id",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Companies_CompanyId",
                table: "CompanyWorkAssignments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LLPWorkAssignments_CompanyWorkAssignments_Id",
                table: "LLPWorkAssignments",
                column: "Id",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StrikeOffWorkAssignments_CompanyWorkAssignments_Id",
                table: "StrikeOffWorkAssignments",
                column: "Id",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentUserMappings_CompanyWorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMappings",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
