using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class secstrikeofftemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecStrikeOffTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoneByUserId = table.Column<int>(type: "int", nullable: true),
                    PenaltiesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RevisedPenaltiesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SSMPenaltiesAppealDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMPenaltiesPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMDocSentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecStrikeOffTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecStrikeOffTemplates_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecStrikeOffTemplates_Users_DoneByUserId",
                        column: x => x.DoneByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecStrikeOffTemplates_ClientId",
                table: "SecStrikeOffTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SecStrikeOffTemplates_DoneByUserId",
                table: "SecStrikeOffTemplates",
                column: "DoneByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecStrikeOffTemplates");
        }
    }
}
