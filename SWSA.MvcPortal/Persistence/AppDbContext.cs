using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Company> Companies { get; set; }
    internal DbSet<CompanyStaff> CompanyStaffs { get; set; }
    internal DbSet<CompanyDepartment> CompanyDepartments { get; set; }
    internal DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }
    internal DbSet<CompanyOfficialContact> CompanyOfficialContacts { get; set; }
    internal DbSet<CompanyOwner> CompanyOwners { get; set; }
    internal DbSet<CompanyType> CompanyTypes { get; set; }
    internal DbSet<CompanyWorkAssignment> CompanyWorkAssignments { get; set; }
    internal DbSet<CompanyWorkProgress> CompanyWorkProgresses { get; set; }
    internal DbSet<CompanyComplianceDate> CompanyComplianceDates { get; set; }
    internal DbSet<Department> Departments { get; set; }
    internal DbSet<DocumentRecord> DocumentRecords { get; set; }
    internal DbSet<MsicCode> MsicCodes { get; set; }
    internal DbSet<SystemNotificationLog> SystemNotificationLogs { get; set; }

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

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasMany(c => c.CompanyStaffs)
            .WithOne(cc => cc.Company)
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict); //When deleting a company, please perform delete all contact before delete company;

            entity.HasOne(c => c.CompanyComplianceDate)
            .WithOne(ccd => ccd.Company)
            .HasForeignKey<CompanyComplianceDate>(ccd => ccd.CompanyId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete, enforce manual cleanup
        });

        modelBuilder.Entity<CompanyStaff>(entity =>
        {
            entity.HasIndex(tc => new { tc.StaffId }).IsUnique();

            entity.Property(e => e.StaffId)
         .HasComputedColumnSql(
              "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
              stored: true);
        });

        modelBuilder.Entity<CompanyComplianceDate>()
            .HasIndex(c => new { c.CompanyId });


        modelBuilder.Entity<CompanyDepartment>(entity =>
        {
            entity.HasMany(c => c.CompanyStaffs)
            .WithOne(cc => cc.CompanyDepartment)
            .HasForeignKey(cc => cc.CompanyDepartmentId)
            .OnDelete(DeleteBehavior.SetNull); //When deleting a department, set the foreign key to null instead of deleting the contact

        });

        modelBuilder.Entity<CompanyWorkAssignment>()
            .HasOne(x => x.Progress)
            .WithOne(p => p.WorkAssignment)
            .HasForeignKey<CompanyWorkProgress>(p => p.WorkAssignmentId);

        modelBuilder.Entity<SystemNotificationLog>(entity =>
        {
            entity.HasIndex(c => new { c.CreatedAt, c.Channel });
            entity.HasIndex(c => new { c.CreatedAt, c.TemplateCode });
        });

        base.OnModelCreating(modelBuilder);
    }
}
