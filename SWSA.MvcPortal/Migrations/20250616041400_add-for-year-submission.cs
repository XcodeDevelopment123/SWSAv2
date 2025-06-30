using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class addforyearsubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForYear",
                table: "LLPSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForYear",
                table: "CompanyStrikeOffSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForYear",
                table: "AuditSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForYear",
                table: "AnnualReturnSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "LLPSubmissions");

            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "CompanyStrikeOffSubmissions");

            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "AuditSubmissions");

            migrationBuilder.DropColumn(
                name: "ForYear",
                table: "AnnualReturnSubmissions");
        }
    }
}
