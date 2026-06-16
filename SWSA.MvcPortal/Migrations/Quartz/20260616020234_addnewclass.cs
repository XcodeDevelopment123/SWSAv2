using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations.Quartz
{
    /// <inheritdoc />
    public partial class addnewclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_StaffId",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "AdDueDate",
                table: "SecDeptTaskTemplates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearInput",
                table: "SecDeptTaskTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffId",
                table: "Users",
                column: "StaffId",
                unique: true,
                filter: "[StaffId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_StaffId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdDueDate",
                table: "SecDeptTaskTemplates");

            migrationBuilder.DropColumn(
                name: "YearInput",
                table: "SecDeptTaskTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffId",
                table: "Users",
                column: "StaffId",
                unique: true,
                filter: "[Id] IS NOT NULL");
        }
    }
}
