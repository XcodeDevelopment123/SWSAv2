using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations.Quartz
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AmountTaxPayable",
                table: "BP34",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalTaxComplete",
                table: "BP34",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAmount",
                table: "BP34",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceDate",
                table: "BP34",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewTaxComplete",
                table: "BP34",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceAmount",
                table: "BP33",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountTaxPayable",
                table: "BP34");

            migrationBuilder.DropColumn(
                name: "FinalTaxComplete",
                table: "BP34");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "BP34");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "BP34");

            migrationBuilder.DropColumn(
                name: "ReviewTaxComplete",
                table: "BP34");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "BP33");
        }
    }
}
