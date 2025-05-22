using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateannualreturnsubmissionrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualReturnSubmissions_CompanyWorkAssignments_WorkAssignmentId",
                table: "AnnualReturnSubmissions",
                column: "WorkAssignmentId",
                principalTable: "CompanyWorkAssignments",
                principalColumn: "Id");
        }
    }
}
