using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence.Seeders;

public class UserSeeder(AppDbContext db) : ISeeder
{

    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        if (db.Users.Any())
        {
            return;
        }

        using var transaction = db.Database.BeginTransaction();
        try
        {

            db.Users.Add(GetDefaultAdmin());

            await db.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private User GetDefaultAdmin()
    {
        return new User()
        {
            Username = "Admin",
            Email = "Admin@swsa.com",
            HashedPassword = PasswordHasher.Hash("admin"),
            FullName = "SWSA Admin",
            PhoneNumber = "+60123456789",
            Department = "Full",
            Role = UserRole.SuperAdmin
        };
    }


}
