using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class addsecdepttasktemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecDeptTaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ARDueDate = table.Column<int>(type: "int", nullable: false),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ARSendToClientDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ARReturnByClientDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADSendToClientDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ADReturnByClientDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecDeptTaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecDeptTaskTemplates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecDeptTaskTemplates_ClientId",
                table: "SecDeptTaskTemplates",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecDeptTaskTemplates");
        }
    }
}
