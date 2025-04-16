using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateworkassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthToDo",
                table: "CompanyWorkAssignments");

            migrationBuilder.DropColumn(
                name: "YearToDo",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "DocumentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "CompanyWorkAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "DocumentRecords");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "CompanyWorkAssignments");

            migrationBuilder.AddColumn<int>(
                name: "MonthToDo",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearToDo",
                table: "CompanyWorkAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
