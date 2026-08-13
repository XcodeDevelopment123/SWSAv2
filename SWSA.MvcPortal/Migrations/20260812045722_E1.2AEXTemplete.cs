using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class E12AEXTemplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateMgmtAccAvailable",
                table: "TX3",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mitrssubmitted",
                table: "TX3",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PenaltiesRm",
                table: "TX3",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearToDo",
                table: "TX3",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AuditExemption",
                table: "AEXTemplates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPostAuditBinded",
                table: "AEXTemplates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportType",
                table: "AEXTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SigningFirm",
                table: "AEXTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetTaxWorkDate",
                table: "AEXTemplates",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateMgmtAccAvailable",
                table: "TX3");

            migrationBuilder.DropColumn(
                name: "Mitrssubmitted",
                table: "TX3");

            migrationBuilder.DropColumn(
                name: "PenaltiesRm",
                table: "TX3");

            migrationBuilder.DropColumn(
                name: "YearToDo",
                table: "TX3");

            migrationBuilder.DropColumn(
                name: "AuditExemption",
                table: "AEXTemplates");

            migrationBuilder.DropColumn(
                name: "IsPostAuditBinded",
                table: "AEXTemplates");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "AEXTemplates");

            migrationBuilder.DropColumn(
                name: "SigningFirm",
                table: "AEXTemplates");

            migrationBuilder.DropColumn(
                name: "TargetTaxWorkDate",
                table: "AEXTemplates");
        }
    }
}
