using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateaextemplatetaxstrikeoff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AEXBcklogs",
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
                    table.PrimaryKey("PK_AEXBcklogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AEXBcklogs_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AEXTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PersonInChargeId = table.Column<int>(type: "int", nullable: true),
                    Database = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    YearEndToDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuarterToDo = table.Column<int>(type: "int", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AuditFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateBilled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFieldWorkDays = table.Column<int>(type: "int", nullable: false),
                    AuditWIPResult = table.Column<int>(type: "int", nullable: true),
                    IsAccSetupComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsAccSummaryComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsAuditPlanningComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsAuditExecutionComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsExecutionAuditComplete = table.Column<bool>(type: "bit", nullable: false),
                    FirstReviewSendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstReviewEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstReviewResult = table.Column<int>(type: "int", nullable: true),
                    SecondReviewSendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondReviewEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondReviewResult = table.Column<int>(type: "int", nullable: true),
                    SecondReviewFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeDateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeAuditReportReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeReportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeDirectorsReportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorDateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorFollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorDateReceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorCommOfOathsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePassToTaxDept = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecSSMDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePassToSecDept = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostAuditDateBinded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostAuditDespatchDateToClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEXTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AEXTemplates_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AEXTemplates_Users_PersonInChargeId",
                        column: x => x.PersonInChargeId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaxStrikeOffTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    IRBPenaltiesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PenaltiesAppealDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PenaltiesPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccountWorkComplete = table.Column<bool>(type: "bit", nullable: false),
                    FormESubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormCSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsClientCopySent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxStrikeOffTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxStrikeOffTemplates_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AEXBcklogs_ClientId",
                table: "AEXBcklogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AEXTemplates_ClientId",
                table: "AEXTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AEXTemplates_PersonInChargeId",
                table: "AEXTemplates",
                column: "PersonInChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxStrikeOffTemplates_ClientId",
                table: "TaxStrikeOffTemplates",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AEXBcklogs");

            migrationBuilder.DropTable(
                name: "AEXTemplates");

            migrationBuilder.DropTable(
                name: "TaxStrikeOffTemplates");
        }
    }
}
