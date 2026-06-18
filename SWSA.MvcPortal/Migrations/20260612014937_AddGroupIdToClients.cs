using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace SWSA.MvcPortal.Migrations
{
    public partial class AddGroupIdToClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'Clients' AND COLUMN_NAME = 'GroupId'
                )
                BEGIN
                    ALTER TABLE [Clients] ADD [GroupId] int NULL;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (
                    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'Clients' AND COLUMN_NAME = 'GroupId'
                )
                BEGIN
                    ALTER TABLE [Clients] DROP COLUMN [GroupId];
                END
            ");
        }
    }
}