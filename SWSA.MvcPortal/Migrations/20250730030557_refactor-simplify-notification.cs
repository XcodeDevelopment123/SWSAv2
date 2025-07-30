using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class refactorsimplifynotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SystemNotificationLogs_CreatedAt_TemplateCode",
                table: "SystemNotificationLogs");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "SystemNotificationLogs");

            migrationBuilder.DropColumn(
                name: "TemplateCode",
                table: "SystemNotificationLogs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Reminders",
                newName: "TargetAt");

            migrationBuilder.RenameIndex(
                name: "IX_Reminders_DueDate_Status",
                table: "Reminders",
                newName: "IX_Reminders_TargetAt_Status");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ScheduledWorkAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ScheduledWorkAllocations");

            migrationBuilder.RenameColumn(
                name: "TargetAt",
                table: "Reminders",
                newName: "DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_Reminders_TargetAt_Status",
                table: "Reminders",
                newName: "IX_Reminders_DueDate_Status");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "SystemNotificationLogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateCode",
                table: "SystemNotificationLogs",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reminders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotificationLogs_CreatedAt_TemplateCode",
                table: "SystemNotificationLogs",
                columns: new[] { "CreatedAt", "TemplateCode" });
        }
    }
}
