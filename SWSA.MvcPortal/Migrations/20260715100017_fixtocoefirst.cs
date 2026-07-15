using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class fixtocoefirst : Migration
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
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfBagBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ByWhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSendToAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfBoxBag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ByWhoam2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadLetter2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Clients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocFr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfBoxBag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ByWhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfbox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ByWhom2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadLetter2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeIncoming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefDescritions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoneOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerInCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIrbemailLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsCorrepondence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeIncoming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefDescritions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoneOn = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerInchrage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIrbemailLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsCorrepondence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A33B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEX41",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuartertoAudit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yeattodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovetoActiveSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovetoBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First18mthsdue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditedAccDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoSec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstRev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstNetProfit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEX41", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEX51",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First18mthDue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocInwardsDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days1_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFieldWorksDays1_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSenttoKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedfrKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhomeetClientDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasstoTaxDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePassToSecDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Binded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DespatachDateClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metric421 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEX51", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AEX52",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yeartodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoofDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditCompletion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkdateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkendDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkresultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Final = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuartertoAudit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovetoAex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovetoBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First18mthdue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AfsdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoSec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditStaff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstRev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcctngWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YetoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuartertoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthDue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocInwardsDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaysDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonePercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent1_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate1_7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days1_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentToKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewResultofDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceiveFromKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalReviewDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Metric421 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoMeetClientDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSenttoClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceiveBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasstoDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePasstoSecDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Binded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DespatchDateToClient = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeartoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quartertodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBilled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFieldwkDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalResultOverUnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccSetup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditPlanning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditCompleion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkdateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkendDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KkofDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalReviewDays = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReviewed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSentToKk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceivedAfs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofAfs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofDirectorsRept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbrsgenerated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlwUpDates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First18mthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommofOathsDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YmdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CirculationAfsdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B11", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aexstatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssm18mthDue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedSendDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSend2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aexstatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssm18mthDue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TStartAccWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aexstatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ssm18mthDue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TStartAccWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B32", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B34",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullSet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reveiw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxOnly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TStartAccWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TStartWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B35", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B36",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndividualTaxPayer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TStartWk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRemind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TFinalText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B36", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP21",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstmthToDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtensionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthToDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YetoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP26", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BP31",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YetoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDocIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstMthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllocateToWkSch = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocReceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxArdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Staff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePassToAudit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTaxSubmited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedBacklog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxComputation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortingFiling = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyinToExcel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewWorkingAcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftFinancialStatement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxComFinalSignByClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountTaxPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileDraft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileviaSpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountRm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxComFinalSignByClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountofTaxPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileDraft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxReferennceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeofForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicesNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftTaxCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxComFinalSignbyClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountTaxPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileDraft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfileFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRefferanceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeofForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocDespatchDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTaxComplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTaxComplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountTaxPayable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceAmount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BP34", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "S13A",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecFileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YetoDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccmthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditMthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YrMthDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Circulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AfssubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Grouping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecFileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccmthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmextensionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccReady = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnniversaryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSendtoClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReturned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YrMthdueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CirculationAfsduedate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MbrsreceivedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OntimeLate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForLate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    SecFileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Co = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveCoActivitySize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmextensionDateforAcc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSendtoClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReturned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Ref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompleteDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoneBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenaltiesRm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevisedPenalties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SOffDocsendtoClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmstrikeoffDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePassToTaxDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_S16", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstQuartertodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatemgmtAccAvailbale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnLanalysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaTaxCompu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftFormC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPayableRm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCompuCa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftToClientSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftToClientReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmitedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Printing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Despatch = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Irbpenalties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccWkDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormEsubmitDare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSentCopy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aexot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rakc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Btm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstMthTodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferToWiptx3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetProfit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocPassFrAuditDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateMgmtAccAvailable = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aexot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rakc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Btm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxDueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnLanalysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaTaxCompu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPayable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCompCa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxPaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Printing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Despatch = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SsmsubmissionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSoff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReceiveFrSecDept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Irbpenalties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppealDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccWkDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormCsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormEsubmitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountRm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientCopySent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobCompletedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX4", x => x.Id);
                });
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
                name: "TX1");

            migrationBuilder.DropTable(
                name: "TX1B");

            migrationBuilder.DropTable(
                name: "TX2");

            migrationBuilder.DropTable(
                name: "TX3");

            migrationBuilder.DropTable(
                name: "TX4");
        }
    }
}
