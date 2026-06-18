using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations.Quartz
{
    /// <inheritdoc />
    public partial class fixerrorclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateSent1_6",
                table: "AT31",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days1_8",
                table: "AT31",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndDate1_7",
                table: "AT31",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Metric421",
                table: "AT31",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalReviewDays",
                table: "AT31",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days1_3",
                table: "AEX51",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Metric421",
                table: "AEX51",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalFieldWorksDays1_4",
                table: "AEX51",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSent1_6",
                table: "AT31");

            migrationBuilder.DropColumn(
                name: "Days1_8",
                table: "AT31");

            migrationBuilder.DropColumn(
                name: "EndDate1_7",
                table: "AT31");

            migrationBuilder.DropColumn(
                name: "Metric421",
                table: "AT31");

            migrationBuilder.DropColumn(
                name: "TotalReviewDays",
                table: "AT31");

            migrationBuilder.DropColumn(
                name: "Days1_3",
                table: "AEX51");

            migrationBuilder.DropColumn(
                name: "Metric421",
                table: "AEX51");

            migrationBuilder.DropColumn(
                name: "TotalFieldWorksDays1_4",
                table: "AEX51");
        }
    }
}
