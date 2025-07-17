using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SystemNotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TemplateCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ResultMessage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotificationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: false, computedColumnSql: "'StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)", stored: true),
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
                    Role = table.Column<int>(type: "int", nullable: false)
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
                    EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "ClientWorkAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceScope = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: true),
                    CompanyStatus = table.Column<int>(type: "int", nullable: true),
                    AuditStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWorkAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientWorkAllocations_Clients_ClientId",
                        column: x => x.ClientId,
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
                name: "WorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    WorkType = table.Column<int>(type: "int", nullable: false),
                    ServiceScope = table.Column<int>(type: "int", nullable: false),
                    InternalNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: false),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignments_Clients_ClientId",
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
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeSummaryJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavigateUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "AnnualReturnWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedARDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfAnnualReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualReturnWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualReturnWorkAssignments_WorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstYearAccountStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedCirculation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    ReasonForLate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditWorkAssignments_WorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LLPWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SSMExtensionDateForAcc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LLPWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LLPWorkAssignments_WorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrikeOffWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IRBSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMStrikeOffDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrikeOffWorkAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrikeOffWorkAssignments_WorkAssignments_Id",
                        column: x => x.Id,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAssignmentUserMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAssignmentUserMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentUserMappings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkAssignmentUserMappings_WorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeTakenInDays = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProgressNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsJobCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsBacklog = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProgresses_WorkAssignments_WorkAssignmentId",
                        column: x => x.WorkAssignmentId,
                        principalTable: "WorkAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkAllocations_ClientId",
                table: "ClientWorkAllocations",
                column: "ClientId");

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
                name: "IX_SystemAuditLogs_PerformedByUserId",
                table: "SystemAuditLogs",
                column: "PerformedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotificationLogs_CreatedAt_Channel",
                table: "SystemNotificationLogs",
                columns: new[] { "CreatedAt", "Channel" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotificationLogs_CreatedAt_TemplateCode",
                table: "SystemNotificationLogs",
                columns: new[] { "CreatedAt", "TemplateCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffId",
                table: "Users",
                column: "StaffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignments_ClientId",
                table: "WorkAssignments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMappings_UserId",
                table: "WorkAssignmentUserMappings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAssignmentUserMappings_WorkAssignmentId_UserId",
                table: "WorkAssignmentUserMappings",
                columns: new[] { "WorkAssignmentId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkProgresses_WorkAssignmentId",
                table: "WorkProgresses",
                column: "WorkAssignmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualReturnWorkAssignments");

            migrationBuilder.DropTable(
                name: "AuditWorkAssignments");

            migrationBuilder.DropTable(
                name: "ClientWorkAllocations");

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
                name: "LLPWorkAssignments");

            migrationBuilder.DropTable(
                name: "OfficialContacts");

            migrationBuilder.DropTable(
                name: "ScheduledJobs");

            migrationBuilder.DropTable(
                name: "SdnBhdClients");

            migrationBuilder.DropTable(
                name: "StrikeOffWorkAssignments");

            migrationBuilder.DropTable(
                name: "SystemAuditLogs");

            migrationBuilder.DropTable(
                name: "SystemNotificationLogs");

            migrationBuilder.DropTable(
                name: "WorkAssignmentUserMappings");

            migrationBuilder.DropTable(
                name: "WorkProgresses");

            migrationBuilder.DropTable(
                name: "MsicCodes");

            migrationBuilder.DropTable(
                name: "BaseCompanies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkAssignments");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
