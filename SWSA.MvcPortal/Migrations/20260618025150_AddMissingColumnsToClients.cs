using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnsToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ADDueDate",
                table: "SecDeptTaskTemplates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearInput",
                table: "SecDeptTaskTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentEngagementData",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessNature",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientRating",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyStatus",
                table: "BaseCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyStatusReason",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditRating",
                table: "BaseCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForeignOwned",
                table: "BaseCompanies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrincipalActivity",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceSelected",
                table: "BaseCompanies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDueDate",
                table: "SecDeptTaskTemplates");

            migrationBuilder.DropColumn(
                name: "YearInput",
                table: "SecDeptTaskTemplates");

            migrationBuilder.DropColumn(
                name: "AppointmentEngagementData",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "BusinessNature",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "ClientRating",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "CompanyStatus",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "CompanyStatusReason",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "CreditRating",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "ForeignOwned",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "PrincipalActivity",
                table: "BaseCompanies");

            migrationBuilder.DropColumn(
                name: "ServiceSelected",
                table: "BaseCompanies");
        }
    }
}
