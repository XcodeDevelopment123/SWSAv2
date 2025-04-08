namespace SWSA.MvcPortal.Persistence.Seeders;

public class SeederManager(IEnumerable<ISeeder> seeders)
{
    public async Task SeedAll()
    {
        foreach (var seeder in seeders)
        {
            await seeder.Seed();
        }
    }
}
