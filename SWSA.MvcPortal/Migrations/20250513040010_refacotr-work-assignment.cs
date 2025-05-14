using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refacotrworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyDepartment_Companies_CompanyId",
                table: "UserCompanyDepartment");

            migrationBuilder.RenameColumn(
                name: "AssignedStaffId",
                table: "CompanyWorkAssignments",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyWorkAssignments_AssignedStaffId",
                table: "CompanyWorkAssignments",
                newName: "IX_CompanyWorkAssignments_AssignedUserId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkAssignments_CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                column: "CompanyDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedUserId",
                table: "CompanyWorkAssignments",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyDepartment_Companies_CompanyId",
                table: "UserCompanyDepartment",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_Users_AssignedUserId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyDepartment_Companies_CompanyId",
                table: "UserCompanyDepartment");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWorkAssignments_CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "CompanyWorkAssignments",
                newName: "AssignedStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyWorkAssignments_AssignedUserId",
                table: "CompanyWorkAssignments",
                newName: "IX_CompanyWorkAssignments_AssignedStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "CompanyStaffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyDepartment_Companies_CompanyId",
                table: "UserCompanyDepartment",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
