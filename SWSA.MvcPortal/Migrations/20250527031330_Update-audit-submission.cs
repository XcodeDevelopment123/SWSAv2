using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class Updateauditsubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccDueDate",
                table: "AuditSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "AuditSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstYearAccountStart",
                table: "AuditSubmissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLate",
                table: "AuditSubmissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForLate",
                table: "AuditSubmissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargettedCirculation",
                table: "AuditSubmissions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccDueDate",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "FirstYearAccountStart",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "IsLate",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "ReasonForLate",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "TargettedCirculation",
                table: "AuditSubmissions");
        }
    }
}
