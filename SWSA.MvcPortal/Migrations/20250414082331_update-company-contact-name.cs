using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanycontactname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCommunicationContacts_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyCommunicationContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyCommunicationContacts_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCommunicationContacts",
                table: "CompanyCommunicationContacts");

            migrationBuilder.RenameTable(
                name: "CompanyCommunicationContacts",
                newName: "CompanyStaffs");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCommunicationContacts_StaffId",
                table: "CompanyStaffs",
                newName: "IX_CompanyStaffs_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCommunicationContacts_CompanyId",
                table: "CompanyStaffs",
                newName: "IX_CompanyStaffs_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCommunicationContacts_CompanyDepartmentId",
                table: "CompanyStaffs",
                newName: "IX_CompanyStaffs_CompanyDepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "CompanyStaffs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyStaffs",
                table: "CompanyStaffs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStaffs_Companies_CompanyId",
                table: "CompanyStaffs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStaffs_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyStaffs",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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
                name: "FK_CompanyStaffs_Companies_CompanyId",
                table: "CompanyStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStaffs_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyStaffs_AssignedStaffId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyStaffs",
                table: "CompanyStaffs");

            migrationBuilder.RenameTable(
                name: "CompanyStaffs",
                newName: "CompanyCommunicationContacts");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStaffs_StaffId",
                table: "CompanyCommunicationContacts",
                newName: "IX_CompanyCommunicationContacts_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStaffs_CompanyId",
                table: "CompanyCommunicationContacts",
                newName: "IX_CompanyCommunicationContacts_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyStaffs_CompanyDepartmentId",
                table: "CompanyCommunicationContacts",
                newName: "IX_CompanyCommunicationContacts_CompanyDepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "CompanyCommunicationContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCommunicationContacts",
                table: "CompanyCommunicationContacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCommunicationContacts_Companies_CompanyId",
                table: "CompanyCommunicationContacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCommunicationContacts_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyCommunicationContacts",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyCommunicationContacts_AssignedStaffId",
                table: "CompanyWorkAssignments",
                column: "AssignedStaffId",
                principalTable: "CompanyCommunicationContacts",
                principalColumn: "Id");
        }
    }
}
