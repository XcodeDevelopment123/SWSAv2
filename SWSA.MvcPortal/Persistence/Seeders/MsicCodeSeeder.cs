
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Systems;

namespace SWSA.MvcPortal.Persistence.Seeders;

public class MsicCodeSeeder(AppDbContext db) : ISeeder
{
    public async Task Seed()
    {
        if (!await db.Database.CanConnectAsync())
        {
            return;
        }

        if (await db.MsicCodes.AnyAsync())
            return;

        using var transaction = db.Database.BeginTransaction();
        try
        {
            var msicCodes = await GetDefaultMsicCodes();

            await db.AddRangeAsync(msicCodes);
            await db.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task<List<MsicCode>> GetDefaultMsicCodes()
    {
        var json = await File.ReadAllTextAsync("Datas/MSICcodes-2025-04-07.json");
        var datas = JsonConvert.DeserializeObject<List<MsicCode>>(json);

        return datas ?? new List<MsicCode>();
    }
}
