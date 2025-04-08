using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.Seeders;

public class DepartmentSeeder(AppDbContext db) : ISeeder
{
    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        if (db.Departments.Any())
        {
            return;
        }

        using var transaction = db.Database.BeginTransaction();
        try
        {

            db.Departments.AddRange(GetDefaultDepartments());

            await db.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private List<Department> GetDefaultDepartments()
    {
        return
            [
            new(Departments.Account),
            new(Departments.Audit),
            new(Departments.Tax),
            new(Departments.Secretary),
        ];
    }
}

