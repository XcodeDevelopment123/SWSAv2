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
    internal DbSet<CompanyOfficialContact> CompanyOfficialContacts { get; set; }
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


        modelBuilder.Entity<Company>()
            .HasMany(c => c.CommunicationContacts)
            .WithOne(cc => cc.Company)
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict); //When deleting a company, please perform delete all contact before delete company

        modelBuilder.Entity<CompanyDepartment>()
            .HasMany(c=>c.CommunicationContacts)
            .WithOne(cc => cc.Department)
            .HasForeignKey(cc => cc.CompanyDepartmentId)
            .OnDelete(DeleteBehavior.SetNull); //When deleting a department, set the foreign key to null instead of deleting the contact

        base.OnModelCreating(modelBuilder);
    }
}
