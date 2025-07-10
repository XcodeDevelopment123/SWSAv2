using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatedocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_CompanyWorkAssignments_WorkAssignmentId",
                table: "DocumentRecords");

            migrationBuilder.RenameColumn(
                name: "WorkAssignmentId",
                table: "DocumentRecords",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentRecords_WorkAssignmentId",
                table: "DocumentRecords",
                newName: "IX_DocumentRecords_CompanyId");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "DocumentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadLetter",
                table: "DocumentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_Companies_CompanyId",
                table: "DocumentRecords",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_Companies_CompanyId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "UploadLetter",
                table: "DocumentRecords");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "DocumentRecords",
                newName: "WorkAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentRecords_CompanyId",
                table: "DocumentRecords",
                newName: "IX_DocumentRecords_WorkAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_CompanyWorkAssignments_WorkAssignmentId",
                table: "DocumentRecords",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
