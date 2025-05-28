using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class udpatesubmissionarllp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "AnnualReturnSubmissions");

            migrationBuilder.AddColumn<DateTime>(
                name: "ARDueDate",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ARSubmitDate",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountSubmitDate",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturnedByClient",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSentToClient",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SSMExtensionDateForAcc",
                table: "LLPSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ARDueDate",
                table: "AnnualReturnSubmissions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ARDueDate",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "ARSubmitDate",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "AccountSubmitDate",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "DateReturnedByClient",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "DateSentToClient",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "SSMExtensionDateForAcc",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "ARDueDate",
                table: "AnnualReturnSubmissions");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AnnualReturnSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
