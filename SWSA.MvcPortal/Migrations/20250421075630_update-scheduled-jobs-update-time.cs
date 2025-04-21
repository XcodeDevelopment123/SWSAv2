using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updatescheduledjobsupdatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobKey",
                table: "ScheduledJobs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JobGroup",
                table: "ScheduledJobs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "ScheduledJobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_JobGroup_JobKey",
                table: "ScheduledJobs",
                columns: new[] { "JobGroup", "JobKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledJobs_JobKey",
                table: "ScheduledJobs",
                column: "JobKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduledJobs_JobGroup_JobKey",
                table: "ScheduledJobs");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledJobs_JobKey",
                table: "ScheduledJobs");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "ScheduledJobs");

            migrationBuilder.AlterColumn<string>(
                name: "JobKey",
                table: "ScheduledJobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "JobGroup",
                table: "ScheduledJobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
