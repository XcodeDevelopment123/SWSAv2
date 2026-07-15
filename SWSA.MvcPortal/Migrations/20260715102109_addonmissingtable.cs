using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class addonmissingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AEX12",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfitLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentKuching = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDateKuching = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnderKuching = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDateKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnderKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Final = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentToKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfDirectorRept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentSigning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlwUpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommOfOathsDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassToTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePassToSecDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBinded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DespatchDateToClient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEX12", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEX42",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterToDoAudit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearToDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveToActiveSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonWhyBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEX42", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT11",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhichDb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfitLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFieldWkDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnderKuching = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDateKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnderKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalReviewDaysKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentToKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfDirectorsRept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentSigning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlwUpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedSigning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommOfOathsDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassToTaxDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePassToSecDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBinded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DespatchDateToClient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT11", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT22",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterToDoAudit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearToDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveToActiveAexSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonWhyBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT22", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstQuarterTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateMgmtAccAvailable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnLanalysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanTaxCompu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftFormC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPayableRm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCompCa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormC1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmitedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Printing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Despatch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AEX12");

            migrationBuilder.DropTable(
                name: "AEX42");

            migrationBuilder.DropTable(
                name: "AT11");

            migrationBuilder.DropTable(
                name: "AT22");

            migrationBuilder.DropTable(
                name: "FormC");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
