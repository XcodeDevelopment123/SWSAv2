using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.Seeders;

public class CompanyTypeSeeder(AppDbContext db) : ISeeder
{
    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        if (db.CompanyTypes.Any())
        {
            return;
        }

        using var transaction = db.Database.BeginTransaction();
        try
        {

            db.CompanyTypes.AddRange(GetDefaultCompanyTypes());

            await db.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private List<CompanyType> GetDefaultCompanyTypes()
    {
        return [
            new(CompanyTypes.SdnBhd),
            new(CompanyTypes.LLP),
            new(CompanyTypes.Enterprise),
            new(CompanyTypes.Individual),
            ];
    }
}
