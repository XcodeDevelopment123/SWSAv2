using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanyactivitysize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditBacklogSchedules_Clients_ClientId",
                table: "AuditBacklogSchedules");

            migrationBuilder.AddColumn<int>(
                name: "ActivitySize",
                table: "BaseCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditBacklogSchedules_BaseCompanies_ClientId",
                table: "AuditBacklogSchedules",
                column: "ClientId",
                principalTable: "BaseCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditBacklogSchedules_BaseCompanies_ClientId",
                table: "AuditBacklogSchedules");

            migrationBuilder.DropColumn(
                name: "ActivitySize",
                table: "BaseCompanies");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditBacklogSchedules_Clients_ClientId",
                table: "AuditBacklogSchedules",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
