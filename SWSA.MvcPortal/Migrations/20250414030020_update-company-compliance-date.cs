using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatecompanycompliancedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyComplianceDates_Companies_CompanyId",
                table: "CompanyComplianceDates");

            migrationBuilder.DropColumn(
                name: "DatePassedToAudit",
                table: "CompanyWorkProgresses");

            migrationBuilder.DropColumn(
                name: "DateTaxSubmitted",
                table: "CompanyWorkProgresses");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyComplianceDates_Companies_CompanyId",
                table: "CompanyComplianceDates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyComplianceDates_Companies_CompanyId",
                table: "CompanyComplianceDates");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePassedToAudit",
                table: "CompanyWorkProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTaxSubmitted",
                table: "CompanyWorkProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyComplianceDates_Companies_CompanyId",
                table: "CompanyComplianceDates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
