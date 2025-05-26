using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorworkmonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAssignmentAccountMonths");

            migrationBuilder.DropTable(
                name: "WorkAssignmentAuditMonths");

            migrationBuilder.CreateTable(
                name: "WorkAssignmentMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_WorkAssignmentMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentMonths",
                column: "CompanyWorkAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAssignmentMonths");

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAccountMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WorkAssignmentAuditMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentAuditMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentAuditMonths_CompanyWorkAssignments_CompanyWorkAssignmentId",
                        column: x => x.CompanyWorkAssignmentId,
                        principalTable: "CompanyWorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAccountMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentAccountMonths",
                column: "CompanyWorkAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentAuditMonths_CompanyWorkAssignmentId",
                table: "WorkAssignmentAuditMonths",
                column: "CompanyWorkAssignmentId");
        }
    }
}
