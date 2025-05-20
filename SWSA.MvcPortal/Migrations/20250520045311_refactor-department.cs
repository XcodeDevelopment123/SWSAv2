using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactordepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStaffs_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_CompanyDepartments_CompanyDepartmentId",
                table: "DocumentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanyDepartment_Departments_DepartmentId",
                table: "UserCompanyDepartment");

            migrationBuilder.DropTable(
                name: "CompanyDepartments");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_UserCompanyDepartment_DepartmentId",
                table: "UserCompanyDepartment");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRecords_CompanyDepartmentId",
                table: "DocumentRecords");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWorkAssignments_CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStaffs_CompanyDepartmentId",
                table: "CompanyStaffs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "UserCompanyDepartment");

            migrationBuilder.DropColumn(
                name: "CompanyDepartmentId",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "CompanyDepartmentId",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "CompanyDepartmentId",
                table: "CompanyStaffs");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "UserCompanyDepartment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "UserCompanyDepartment");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "UserCompanyDepartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyDepartmentId",
                table: "DocumentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyDepartmentId",
                table: "CompanyStaffs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDepartments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyDepartment_DepartmentId",
                table: "UserCompanyDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_CompanyDepartmentId",
                table: "DocumentRecords",
                column: "CompanyDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkAssignments_CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                column: "CompanyDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStaffs_CompanyDepartmentId",
                table: "CompanyStaffs",
                column: "CompanyDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartments_CompanyId",
                table: "CompanyDepartments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartments_DepartmentId",
                table: "CompanyDepartments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStaffs_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyStaffs",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWorkAssignments_CompanyDepartments_CompanyDepartmentId",
                table: "CompanyWorkAssignments",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_CompanyDepartments_CompanyDepartmentId",
                table: "DocumentRecords",
                column: "CompanyDepartmentId",
                principalTable: "CompanyDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanyDepartment_Departments_DepartmentId",
                table: "UserCompanyDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
