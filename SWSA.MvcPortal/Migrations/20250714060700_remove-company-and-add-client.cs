using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class removecompanyandaddclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyMsicCodes_Companies_CompanyId",
                table: "CompanyMsicCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyOwners_Companies_CompanyId",
                table: "CompanyOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_Companies_CompanyId",
                table: "DocumentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignments_Companies_CompanyId",
                table: "WorkAssignments");

            migrationBuilder.DropTable(
                name: "CompanyCommunicationContact");

            migrationBuilder.DropTable(
                name: "CompanyComplianceDates");

            migrationBuilder.DropTable(
                name: "CompanyOfficialContacts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_SystemAuditLogs_CompanyId",
                table: "SystemAuditLogs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SystemAuditLogs");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "WorkAssignments",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignments_CompanyId",
                table: "WorkAssignments",
                newName: "IX_WorkAssignments_ClientId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "DocumentRecords",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentRecords_CompanyId",
                table: "DocumentRecords",
                newName: "IX_DocumentRecords_ClientId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "CompanyOwners",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyOwners_CompanyId",
                table: "CompanyOwners",
                newName: "IX_CompanyOwners_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "BaseCompanyId",
                table: "CompanyOwners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearEndMonth = table.Column<int>(type: "int", nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClintType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientWorkAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceScope = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: false),
                    CompanyStatus = table.Column<int>(type: "int", nullable: true),
                    AuditStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWorkAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientWorkAllocations_BaseClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseClient",
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
                        name: "FK_CommunicationContacts_BaseClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseClient",
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
                        name: "FK_IndividualClients_BaseClient_Id",
                        column: x => x.Id,
                        principalTable: "BaseClient",
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
                        name: "FK_OfficialContacts_BaseClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseClient",
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
                        name: "FK_EnterpriseClients_BaseCompany_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompany",
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
                        name: "FK_LLPClients_BaseCompany_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompany",
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
                        name: "FK_SdnBhdClients_BaseCompany_Id",
                        column: x => x.Id,
                        principalTable: "BaseCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOwners_BaseCompanyId",
                table: "CompanyOwners",
                column: "BaseCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkAllocations_ClientId",
                table: "ClientWorkAllocations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationContacts_ClientId",
                table: "CommunicationContacts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialContacts_ClientId",
                table: "OfficialContacts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyMsicCodes_BaseCompany_CompanyId",
                table: "CompanyMsicCodes",
                column: "CompanyId",
                principalTable: "BaseCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyOwners_BaseClient_ClientId",
                table: "CompanyOwners",
                column: "ClientId",
                principalTable: "BaseClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyOwners_BaseCompany_BaseCompanyId",
                table: "CompanyOwners",
                column: "BaseCompanyId",
                principalTable: "BaseCompany",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_BaseClient_ClientId",
                table: "DocumentRecords",
                column: "ClientId",
                principalTable: "BaseClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignments_BaseClient_ClientId",
                table: "WorkAssignments",
                column: "ClientId",
                principalTable: "BaseClient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyMsicCodes_BaseCompany_CompanyId",
                table: "CompanyMsicCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyOwners_BaseClient_ClientId",
                table: "CompanyOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyOwners_BaseCompany_BaseCompanyId",
                table: "CompanyOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentRecords_BaseClient_ClientId",
                table: "DocumentRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAssignments_BaseClient_ClientId",
                table: "WorkAssignments");

            migrationBuilder.DropTable(
                name: "ClientWorkAllocations");

            migrationBuilder.DropTable(
                name: "CommunicationContacts");

            migrationBuilder.DropTable(
                name: "EnterpriseClients");

            migrationBuilder.DropTable(
                name: "IndividualClients");

            migrationBuilder.DropTable(
                name: "LLPClients");

            migrationBuilder.DropTable(
                name: "OfficialContacts");

            migrationBuilder.DropTable(
                name: "SdnBhdClients");

            migrationBuilder.DropTable(
                name: "BaseClient");

            migrationBuilder.DropTable(
                name: "BaseCompany");

            migrationBuilder.DropIndex(
                name: "IX_CompanyOwners_BaseCompanyId",
                table: "CompanyOwners");

            migrationBuilder.DropColumn(
                name: "BaseCompanyId",
                table: "CompanyOwners");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "WorkAssignments",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkAssignments_ClientId",
                table: "WorkAssignments",
                newName: "IX_WorkAssignments_CompanyId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "DocumentRecords",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentRecords_ClientId",
                table: "DocumentRecords",
                newName: "IX_DocumentRecords_CompanyId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "CompanyOwners",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyOwners_ClientId",
                table: "CompanyOwners",
                newName: "IX_CompanyOwners_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "SystemAuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyActivityLevel = table.Column<int>(type: "int", nullable: false),
                    CompanyStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEndMonth = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCommunicationContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCommunicationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCommunicationContact_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyComplianceDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AGMDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnniversaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnnualReturnDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstYearAccountStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyComplianceDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyComplianceDates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyOfficialContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOfficialContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyOfficialContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemAuditLogs_CompanyId",
                table: "SystemAuditLogs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommunicationContact_CompanyId",
                table: "CompanyCommunicationContact",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyComplianceDates_CompanyId",
                table: "CompanyComplianceDates",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOfficialContacts_CompanyId",
                table: "CompanyOfficialContacts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyMsicCodes_Companies_CompanyId",
                table: "CompanyMsicCodes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyOwners_Companies_CompanyId",
                table: "CompanyOwners",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentRecords_Companies_CompanyId",
                table: "DocumentRecords",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAuditLogs_Companies_CompanyId",
                table: "SystemAuditLogs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAssignments_Companies_CompanyId",
                table: "WorkAssignments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
