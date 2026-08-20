using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWSA.MvcPortal.Migrations
{
    /// <inheritdoc />
    public partial class G2AddClassForWholeForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TX5",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearToDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonInCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastYearTaxEstimate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204OneMonthBeforeYE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204ReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204ClientResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204CurrentETP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204DateSubmitIRB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a6thMonthAfterYE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a1stReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a1stClientResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a1stRevisedETP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a1stDateSubmitIRB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a9thMonthAfterYE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a2ndReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a2ndClientResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a2ndRevisedETP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a2ndDateSubmitIRB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a11thMonthAfterYE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a3rdReminderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a3rdClientResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a3rdRevisedETP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cp204a3rdDateSubmitIRB = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX5", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TX5");
        }
    }
}
