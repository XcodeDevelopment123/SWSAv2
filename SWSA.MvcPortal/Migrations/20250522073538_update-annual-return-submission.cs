using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateannualreturnsubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnSubmissions_Companies_CompanyId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AnnualReturnSubmissions_CompanyId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AnnualReturnSubmissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AnnualReturnSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualReturnSubmissions_CompanyId",
                table: "AnnualReturnSubmissions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnSubmissions_Companies_CompanyId",
                table: "AnnualReturnSubmissions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
