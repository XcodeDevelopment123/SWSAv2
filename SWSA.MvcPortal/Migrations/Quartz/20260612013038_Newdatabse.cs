using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations.Quartz
{
    /// <inheritdoc />
    public partial class Newdatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A31A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoOfBagBox = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ByWhom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadLetter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateSendToAD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoOfBoxBag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ByWhoam2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadLetter2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A31A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A31B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocFr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoOfBoxBag = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    ByWhom = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadLetter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoOfbox = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ByWhom2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadLetter2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark2 = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A31B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A32A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeIncoming = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Client = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BriefDescritions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoneOn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A32A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A32B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Client = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OfficerInCharge = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TelExtension = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateIRBemailLetter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DetailsCorrepondence = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A32B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A33A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeIncoming = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Client = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BriefDescritions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoneOn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A33A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "A33B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Client = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OfficerInchrage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TelExtension = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateIRBemailLetter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DetailsCorrepondence = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A33B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEX41",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuartertoAudit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Yeattodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MovetoActiveSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MovetoBacklog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    First18mthsdue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditedAccDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoSec = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Team = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstRev = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstNetProfit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AEX51",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    First18mthDue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocInwardsDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Profit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DonePercent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSenttoKK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceivedfrKK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WhomeetClientDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSentClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceivedBack = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PasstoTaxDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DatePassToSecDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Binded = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DespatachDateClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AEX52",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Yeartodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoofDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditCompletion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewDateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewEndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewResultOverUnder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKDateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKEndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKResultOverUnder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Final = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEX52", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT21",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuartertoAudit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MovetoAEX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MovetoBacklog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    First18mthdue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AFSdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoSec = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditStaff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstRev = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT21", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT31",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuartertoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthDue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocInwardsDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Profit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DaysDone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DonePercent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSentToKK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewResultofDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceiveFromKK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WhoMeetClientDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSenttoClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceiveBack = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PasstoDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DatePasstoSecDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Binded = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DespatchDateToClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT31", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT32",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YeartoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalFieldwkDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FinalResultOverUnder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Assummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditCompleion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewDateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewEndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewOfDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKdateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKendDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KKofDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalReviewDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT32", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT33",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReviewed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSentToKK = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceivedAFS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateofAFS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateofDirectorsRept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MBRSgenerated = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT33", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AT34",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FlwUpDates = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    First18mthDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CommofOathsDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT34", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B11",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    File = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorporationDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YMDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CirculationAFSDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReminderDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EmailSend = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B1.1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEXstatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM18MthDue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM_TAX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetedSendDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSend = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetedReminder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSend2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetedCall = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetedFinalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B31",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEXstatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM18MthDue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM_TAX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_startAccWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Call = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_FinalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B31", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B32",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEXstatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM18MthDue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSM_TAX = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_startAccWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Call = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_FinalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "B34",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SingleEntry = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FullSet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Reveiw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxOnly = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_startAccWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Call = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_FinalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B34", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B35",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_startWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_call = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_finalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B35", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B36",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IndividualTaxPayer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PIC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_startWk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Date = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_Call = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    T_FinalText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BP21",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TINnumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ESTmthToDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP21", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP22",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TINnumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExtensionDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthToDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP22", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP23",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegistrationDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TINnumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Login = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobService = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP23", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP24",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitSize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP24", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP25",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Login = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobServices = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP25", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP26",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Login = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobServices = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BP31",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TINnumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstMthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP31", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP32",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobServices = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocReceivedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxARdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TimeTaken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DatePassToAudit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateTaxSubmited = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompletedBacklog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SingleEntry = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxComputation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SortingFiling = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KeyinToExcel = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewWorkingAcc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftFinancialStatement = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxComFinalSignByClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AmountTaxPay = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileDraft = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileFinal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileviaSPC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AmountRM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP32", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP33",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxComFinalSignByClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AmountofTaxPay = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileDraft = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileFinal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxReferennceNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Login = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeofForm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SPC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoicesNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP33", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP34",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxComFinalSignbyClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AmountTaxPay = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileDraft = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EFileFinal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxRefferanceNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Login = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TypeofForm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SPC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP34", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearEndMonth = table.Column<int>(type: "int", nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MsicCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryReferences = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsicCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S13A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SecFileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtoDo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ACCmthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AuditMthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YrMthDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Circulation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ARdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AFSSubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ARSubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S13A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S13B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SecFileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YEtodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ACCmthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMextensionDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccReady = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S13B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S14A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AnniversaryDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ARdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReminderDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateOfAR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ARsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSendtoClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReturned = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S14A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S14B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YrMthdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CirculationAFSduedate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MBRSreceivedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OntimeLate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReasonForLate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S14B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S15",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecFileNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Co = table.Column<string>(name: "Co#", type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMextensionDateforAcc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADdueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ADsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSendtoClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReturned = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S15", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S16",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ref = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompleteDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoneBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompletedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PenaltiesRM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RevisedPenalties = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_OffDocsendtoClient = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMstrikeoffDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DatePassToTaxDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormCSubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S16", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ResultMessage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotificationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstQuartertodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DatemgmtAccAvailbale = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PnLAnalysis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CaTaxCompu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftFormC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxPayableRm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxCompuCA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftToClientSent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftToClientReceived = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxPaymentDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormCSubmitedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fees = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Printing = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Despatch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX1B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IRBPenalties = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoteRemark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccWkDone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormEsubmitDare = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormCsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClientSentCopy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX1B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEXOT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RAKC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BTM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstMthTodo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TransferToWIPTX3 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NetProfit = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocPassFrAuditDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateMgmtAccAvailable = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AEXOT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RAKC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BTM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PnLAnalysis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CaTaxCompu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DraftForm = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxPayable = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxCompCA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Sent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Received = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxPaymentDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormCSubmited = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fees = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Printing = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Despatch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX4",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SSMsubmissionDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateSOff = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateReceiveFrSecDept = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IRBpenalties = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NoteRemark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccWkDone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormCsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FormEsubmitDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AmountRM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClientCopySent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCompletedDate = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true, computedColumnSql: "('StaffId-'+right('000000'+CONVERT([varchar],[Id]),(6)))", stored: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivitySize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseCompanies_Clients_Id",
                        column: x => x.Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationContacts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ICOrPassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualClients_Clients_Id",
                        column: x => x.Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialContacts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentFlow = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    BagOrBoxCount = table.Column<int>(type: "int", nullable: false),
                    UploadLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandledByStaffId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRecords_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRecords_Users_HandledByStaffId",
                        column: x => x.HandledByStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobGroup = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    ScheduleType = table.Column<int>(type: "int", nullable: false),
                    TriggerTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsCustom = table.Column<bool>(type: "bit", nullable: false),
                    RequestPayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExecuteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledJobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SystemAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeSummaryJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedByUserId = table.Column<int>(type: "int", nullable: true),
                    PerformedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemAuditLogs_Users_PerformedByUserId",
                        column: x => x.PerformedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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
                        name: "FK_AuditBacklogSchedules_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "CompanyMsicCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    MsicCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMsicCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMsicCodes_BaseCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyMsicCodes_MsicCodes_MsicCodeId",
                        column: x => x.MsicCodeId,
                        principalTable: "MsicCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientCompanyId = table.Column<int>(type: "int", nullable: false),
                    NamePerIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICOrPassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TaxReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresFormBESubmission = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOwners_BaseCompanies_ClientCompanyId",
                        column: x => x.ClientCompanyId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnterpriseClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpriseClients_BaseCompanies_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LLPClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLPClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLPClients_BaseCompanies_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SdnBhdClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SdnBhdClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SdnBhdClients_BaseCompanies_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecDeptTaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSendToClientDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARReturnByClientDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADSendToClientDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADReturnByClientDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecDeptTaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecDeptTaskTemplates_BaseCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_AuditBacklogSchedules_ClientId",
                table: "AuditBacklogSchedules",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTemplates_ClientId",
                table: "AuditTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTemplates_PersonInChargeId",
                table: "AuditTemplates",
                column: "PersonInChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationContacts_ClientId",
                table: "CommunicationContacts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMsicCodes_CompanyId",
                table: "CompanyMsicCodes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMsicCodes_MsicCodeId",
                table: "CompanyMsicCodes",
                column: "MsicCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOwners_ClientCompanyId",
                table: "CompanyOwners",
                column: "ClientCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_ClientId",
                table: "DocumentRecords",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRecords_HandledByStaffId",
                table: "DocumentRecords",
                column: "HandledByStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialContacts_ClientId",
                table: "OfficialContacts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_JobGroup_JobKey",
                table: "ScheduledJobs",
                columns: new[] { "JobGroup", "JobKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_JobKey",
                table: "ScheduledJobs",
                column: "JobKey");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_UserId",
                table: "ScheduledJobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecDeptTaskTemplates_ClientId",
                table: "SecDeptTaskTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SecStrikeOffTemplates_ClientId",
                table: "SecStrikeOffTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SecStrikeOffTemplates_DoneByUserId",
                table: "SecStrikeOffTemplates",
                column: "DoneByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemAuditLogs_PerformedByUserId",
                table: "SystemAuditLogs",
                column: "PerformedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotificationLogs_CreatedAt_Channel",
                table: "SystemNotificationLogs",
                columns: new[] { "CreatedAt", "Channel" });

            migrationBuilder.CreateIndex(
                name: "IX_TaxStrikeOffTemplates_ClientId",
                table: "TaxStrikeOffTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffId",
                table: "Users",
                column: "StaffId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "A31A");

            migrationBuilder.DropTable(
                name: "A31B");

            migrationBuilder.DropTable(
                name: "A32A");

            migrationBuilder.DropTable(
                name: "A32B");

            migrationBuilder.DropTable(
                name: "A33A");

            migrationBuilder.DropTable(
                name: "A33B");

            migrationBuilder.DropTable(
                name: "AEX41");

            migrationBuilder.DropTable(
                name: "AEX51");

            migrationBuilder.DropTable(
                name: "AEX52");

            migrationBuilder.DropTable(
                name: "AEXBcklogs");

            migrationBuilder.DropTable(
                name: "AEXTemplates");

            migrationBuilder.DropTable(
                name: "AT21");

            migrationBuilder.DropTable(
                name: "AT31");

            migrationBuilder.DropTable(
                name: "AT32");

            migrationBuilder.DropTable(
                name: "AT33");

            migrationBuilder.DropTable(
                name: "AT34");

            migrationBuilder.DropTable(
                name: "AuditBacklogSchedules");

            migrationBuilder.DropTable(
                name: "AuditTemplates");

            migrationBuilder.DropTable(
                name: "B11");

            migrationBuilder.DropTable(
                name: "B2");

            migrationBuilder.DropTable(
                name: "B31");

            migrationBuilder.DropTable(
                name: "B32");

            migrationBuilder.DropTable(
                name: "B34");

            migrationBuilder.DropTable(
                name: "B35");

            migrationBuilder.DropTable(
                name: "B36");

            migrationBuilder.DropTable(
                name: "BP21");

            migrationBuilder.DropTable(
                name: "BP22");

            migrationBuilder.DropTable(
                name: "BP23");

            migrationBuilder.DropTable(
                name: "BP24");

            migrationBuilder.DropTable(
                name: "BP25");

            migrationBuilder.DropTable(
                name: "BP26");

            migrationBuilder.DropTable(
                name: "BP31");

            migrationBuilder.DropTable(
                name: "BP32");

            migrationBuilder.DropTable(
                name: "BP33");

            migrationBuilder.DropTable(
                name: "BP34");

            migrationBuilder.DropTable(
                name: "CommunicationContacts");

            migrationBuilder.DropTable(
                name: "CompanyMsicCodes");

            migrationBuilder.DropTable(
                name: "CompanyOwners");

            migrationBuilder.DropTable(
                name: "DocumentRecords");

            migrationBuilder.DropTable(
                name: "EnterpriseClients");

            migrationBuilder.DropTable(
                name: "IndividualClients");

            migrationBuilder.DropTable(
                name: "LLPClients");

            migrationBuilder.DropTable(
                name: "OfficialContacts");

            migrationBuilder.DropTable(
                name: "S13A");

            migrationBuilder.DropTable(
                name: "S13B");

            migrationBuilder.DropTable(
                name: "S14A");

            migrationBuilder.DropTable(
                name: "S14B");

            migrationBuilder.DropTable(
                name: "S15");

            migrationBuilder.DropTable(
                name: "S16");

            migrationBuilder.DropTable(
                name: "ScheduledJobs");

            migrationBuilder.DropTable(
                name: "SdnBhdClients");

            migrationBuilder.DropTable(
                name: "SecDeptTaskTemplates");

            migrationBuilder.DropTable(
                name: "SecStrikeOffTemplates");

            migrationBuilder.DropTable(
                name: "SystemAuditLogs");

            migrationBuilder.DropTable(
                name: "SystemNotificationLogs");

            migrationBuilder.DropTable(
                name: "TaxStrikeOffTemplates");

            migrationBuilder.DropTable(
                name: "TX1");

            migrationBuilder.DropTable(
                name: "TX1B");

            migrationBuilder.DropTable(
                name: "TX2");

            migrationBuilder.DropTable(
                name: "TX3");

            migrationBuilder.DropTable(
                name: "TX4");

            migrationBuilder.DropTable(
                name: "MsicCodes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BaseCompanies");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
