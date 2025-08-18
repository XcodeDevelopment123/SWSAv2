using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class addaudittemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PersonInChargeId = table.Column<int>(type: "int", nullable: true),
                    Database = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    QuarterToDo = table.Column<int>(type: "int", nullable: false),
                    YearEndToDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AuditFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateBilled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFieldWorkDays = table.Column<int>(type: "int", nullable: false),
                    VarianceType = table.Column<int>(type: "int", nullable: true),
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
                    KualaLumpurOfficeDateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeAuditReportReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeReportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KualaLumpurOfficeDirectorsReportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorDateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorFollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorDateReceived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirectorCommOfOathsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePasswordTaxDept = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecSSMDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePassToSecDept = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostAuditDateBinded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostAuditDespatchDateToClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTemplates_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditTemplates_Users_PersonInChargeId",
                        column: x => x.PersonInChargeId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTemplates_ClientId",
                table: "AuditTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTemplates_PersonInChargeId",
                table: "AuditTemplates",
                column: "PersonInChargeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTemplates");
        }
    }
}
