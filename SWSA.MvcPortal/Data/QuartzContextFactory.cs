using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SWSA.MvcPortal.Data;

/// <summary>
/// Design-time factory for QuartzContext to enable EF Core migrations
/// </summary>
public class QuartzContextFactory : IDesignTimeDbContextFactory<QuartzContext>
{
    public QuartzContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("SwsaConntection");

        var optionsBuilder = new DbContextOptionsBuilder<QuartzContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new QuartzContext(optionsBuilder.Options);
    }
}
