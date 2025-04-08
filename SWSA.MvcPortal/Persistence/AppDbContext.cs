using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Company> Companies { get; set; }
    internal DbSet<CompanyCommunicationContact> CompanyCommunicationContacts { get; set; }
    internal DbSet<CompanyDepartment> CompanyDepartments { get; set; }
    internal DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }
    internal DbSet<CompanyOfficalContact> CompanyOfficalContacts { get; set; }
    internal DbSet<CompanyOwner> CompanyOwners { get; set; }
    internal DbSet<CompanyType> CompanyTypes { get; set; }
    internal DbSet<Department> Departments { get; set; }
    internal DbSet<MsicCode> MsicCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(tc => new { tc.StaffId }).IsUnique();

            entity.Property(e => e.StaffId)
         .HasComputedColumnSql(
              "'StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
              stored: true);
        });

        base.OnModelCreating(modelBuilder);
    }
}
