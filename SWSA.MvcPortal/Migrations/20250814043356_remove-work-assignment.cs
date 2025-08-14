using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class removeworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_ScheduledWorkAllocations_ScheduledWorkAllocationId",
                table: "Reminders");

            migrationBuilder.DropTable(
                name: "AnnualReturnWorkAssignments");

            migrationBuilder.DropTable(
                name: "AuditWorkAssignments");

            migrationBuilder.DropTable(
                name: "ClientWorkAllocations");

            migrationBuilder.DropTable(
                name: "LLPWorkAssignments");

            migrationBuilder.DropTable(
                name: "ScheduledWorkAllocations");

            migrationBuilder.DropTable(
                name: "StrikeOffWorkAssignments");

            migrationBuilder.DropTable(
                name: "WorkAssignmentUserMappings");

            migrationBuilder.DropTable(
                name: "WorkProgresses");

            migrationBuilder.DropTable(
                name: "WorkAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientWorkAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AuditStatus = table.Column<int>(type: "int", nullable: true),
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: true),
                    CompanyStatus = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceScope = table.Column<int>(type: "int", nullable: false)
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
                name: "ScheduledWorkAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedUserId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceScope = table.Column<int>(type: "int", nullable: false),
                    TargetedToStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledWorkAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkAllocations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkAllocations_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
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
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: false),
                    CompanyStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForYear = table.Column<int>(type: "int", nullable: false),
                    InternalNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceScope = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkType = table.Column<int>(type: "int", nullable: false)
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
                name: "AnnualReturnWorkAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfAnnualReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetedARDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    AccDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstYearAccountStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    ReasonForLate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetedCirculation = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    ARDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ARSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountSubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturnedByClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSentToClient = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMExtensionDateForAcc = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IRBSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMStrikeOffDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SSMSubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkAssignmentId = table.Column<int>(type: "int", nullable: false)
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBacklog = table.Column<bool>(type: "bit", nullable: false),
                    IsJobCompleted = table.Column<bool>(type: "bit", nullable: false),
                    ProgressNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeTakenInDays = table.Column<int>(type: "int", nullable: true),
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
                name: "IX_ScheduledWorkAllocations_AssignedUserId",
                table: "ScheduledWorkAllocations",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkAllocations_ClientId",
                table: "ScheduledWorkAllocations",
                column: "ClientId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_ScheduledWorkAllocations_ScheduledWorkAllocationId",
                table: "Reminders",
                column: "ScheduledWorkAllocationId",
                principalTable: "ScheduledWorkAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
