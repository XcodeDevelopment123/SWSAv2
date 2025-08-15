using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class addauditbacklog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSMStatus",
                table: "SecStrikeOffTemplates");

            migrationBuilder.CreateTable(
                name: "AuditBacklogSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    QuarterToDoAudit = table.Column<int>(type: "int", nullable: false),
                    YearToDo = table.Column<int>(type: "int", nullable: false),
                    ReasonForBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditBacklogSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditBacklogSchedules_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditBacklogSchedules_ClientId",
                table: "AuditBacklogSchedules",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditBacklogSchedules");

            migrationBuilder.AddColumn<string>(
                name: "SSMStatus",
                table: "SecStrikeOffTemplates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
