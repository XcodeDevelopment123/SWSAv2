using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class removeiscompleteworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentAuditMonth_CompanyWorkAssignments_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMapping_CompanyWorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMapping_Users_UserId",
                table: "WorkAssignmentUserMapping");

            migrationBuilder.DropTable(
                name: "WorkAssignmentAccountingMonth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssignmentUserMapping",
                table: "WorkAssignmentUserMapping");

            migrationBuilder.DropIndex(
                name: "IX_WorkAssignmentUserMapping_WorkAssignmentId",
                table: "WorkAssignmentUserMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssignmentAuditMonth",
                table: "WorkAssignmentAuditMonth");

            migrationBuilder.DropColumn(
                name: "AccountSubmitDate",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "AnnualReturnSubmittedDate",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "CompanyWorkAssignments");

            migrationBuilder.RenameTable(
                name: "WorkAssignmentUserMapping",
                newName: "WorkAssignmentUserMappings");

            migrationBuilder.RenameTable(
                name: "WorkAssignmentAuditMonth",
                newName: "WorkAssignmentAuditMonths");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignmentUserMapping_UserId",
                table: "WorkAssignmentUserMappings",
                newName: "IX_WorkAssignmentUserMappings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignmentAuditMonth_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonths",
                newName: "IX_WorkAssignmentAuditMonths_CompanyWorkAssignmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "WorkAssignmentUserMappings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssignmentUserMappings",
                table: "WorkAssignmentUserMappings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssignmentAuditMonths",
                table: "WorkAssignmentAuditMonths",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAccountMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentAccountMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentAccountMonths_CompanyWorkAssignments_CompanyWorkAssignmentId",
                        column: x => x.CompanyWorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId_Department",
                table: "WorkAssignmentUserMappings",
                columns: new[] { "WorkAssignmentId", "UserId", "Department" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAccountMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentAccountMonths",
                column: "CompanyWorkAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentAuditMonths_CompanyWorkAssignments_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonths",
                column: "CompanyWorkAssignmentId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentUserMappings_Users_UserId",
                table: "WorkAssignmentUserMappings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentAuditMonths_CompanyWorkAssignments_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonths");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMappings_CompanyWorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignmentUserMappings_Users_UserId",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropTable(
                name: "WorkAssignmentAccountMonths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssignmentUserMappings",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId_Department",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkAssignmentAuditMonths",
                table: "WorkAssignmentAuditMonths");

            migrationBuilder.RenameTable(
                name: "WorkAssignmentUserMappings",
                newName: "WorkAssignmentUserMapping");

            migrationBuilder.RenameTable(
                name: "WorkAssignmentAuditMonths",
                newName: "WorkAssignmentAuditMonth");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignmentUserMappings_UserId",
                table: "WorkAssignmentUserMapping",
                newName: "IX_WorkAssignmentUserMapping_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignmentAuditMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonth",
                newName: "IX_WorkAssignmentAuditMonth_CompanyWorkAssignmentId");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "CompanyWorkAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "WorkAssignmentUserMapping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssignmentUserMapping",
                table: "WorkAssignmentUserMapping",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkAssignmentAuditMonth",
                table: "WorkAssignmentAuditMonth",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAccountingMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMapping_WorkAssignmentId",
                table: "WorkAssignmentUserMapping",
                column: "WorkAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAccountingMonth_CompanyWorkAssignmentId",
                table: "WorkAssignmentAccountingMonth",
                column: "CompanyWorkAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentAuditMonth_CompanyWorkAssignments_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonth",
                column: "CompanyWorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentUserMapping_CompanyWorkAssignments_WorkAssignmentId",
                table: "WorkAssignmentUserMapping",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignmentUserMapping_Users_UserId",
                table: "WorkAssignmentUserMapping",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
