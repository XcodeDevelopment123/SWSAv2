using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanyworkstaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
