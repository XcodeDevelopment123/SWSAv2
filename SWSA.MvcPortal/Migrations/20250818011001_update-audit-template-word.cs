using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class updateaudittemplateword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePasswordTaxDept",
                table: "AuditTemplates",
                newName: "DatePassToTaxDept");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePassToTaxDept",
                table: "AuditTemplates",
                newName: "DatePasswordTaxDept");
        }
    }
}
