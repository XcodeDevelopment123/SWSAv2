using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatesecdepttaskcompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecDeptTaskTemplates_Clients_ClientId",
                table: "SecDeptTaskTemplates");

            migrationBuilder.AddForeignKey(
                name: "FK_SecDeptTaskTemplates_BaseCompanies_ClientId",
                table: "SecDeptTaskTemplates",
                column: "ClientId",
                principalTable: "BaseCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecDeptTaskTemplates_BaseCompanies_ClientId",
                table: "SecDeptTaskTemplates");

            migrationBuilder.AddForeignKey(
                name: "FK_SecDeptTaskTemplates_Clients_ClientId",
                table: "SecDeptTaskTemplates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
