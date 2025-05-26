using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorworkuserpart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId_Department",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId",
                table: "WorkAssignmentUserMappings",
                columns: new[] { "WorkAssignmentId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId",
                table: "WorkAssignmentUserMappings");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "WorkAssignmentUserMappings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId_Department",
                table: "WorkAssignmentUserMappings",
                columns: new[] { "WorkAssignmentId", "UserId", "Department" },
                unique: true);
        }
    }
}
