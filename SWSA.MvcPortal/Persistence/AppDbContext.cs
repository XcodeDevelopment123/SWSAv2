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
    internal DbSet<CompanyWorkAssignment> CompanyWorkAssignments { get; set; }
    internal DbSet<CompanyWorkProgress> CompanyWorkProgresses { get; set; }
    internal DbSet<CompanyComplianceDate> CompanyComplianceDates { get; set; }
    internal DbSet<Department> Departments { get; set; }
    internal DbSet<DocumentRecord> DocumentRecords { get; set; }
    internal DbSet<MsicCode> MsicCodes { get; set; }
    internal DbSet<SystemNotificationLog> SystemNotificationLogs { get; set; }
    internal DbSet<SystemAuditLog> SystemAuditLogs { get; set; }
    internal DbSet<ScheduledJob> ScheduledJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(tc => new { tc.StaffId }).IsUnique();

            entity.Property(e => e.StaffId)
         .HasComputedColumnSql(
              "'StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
              stored: true);

            entity.HasMany(c => c.SystemAuditLogs).WithOne(c => c.PerformedByUser).HasForeignKey(c => c.PerformedByUserId).OnDelete(DeleteBehavior.SetNull);

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

            entity.HasMany(c => c.SystemAuditLogs).WithOne(c => c.Company).HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<CompanyStaff>(entity =>
        {
            entity.HasIndex(tc => new { tc.StaffId }).IsUnique();

            entity.Property(e => e.StaffId)
         .HasComputedColumnSql(
              "'CP-StaffId-' + RIGHT('000000' + CAST([Id] AS VARCHAR), 6)",
              stored: true);

            entity.HasMany(c => c.SystemAuditLogs).WithOne(c => c.PerformedByCompanyStaff).HasForeignKey(c => c.PerformedByCompanyStaffId).OnDelete(DeleteBehavior.SetNull);
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

        //modelBuilder.Entity<SystemAuditLog>(entity =>
        //{
        //    entity.HasOne(e => e.PerformedByUser)
        //        .WithMany()
        //        .HasForeignKey(e => e.PerformedByUserId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    entity.HasOne(e => e.PerformedByCompanyStaff)
        //        .WithMany()
        //        .HasForeignKey(e => e.PerformedByCompanyStaffId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    entity.HasOne(e => e.Company)
        //        .WithMany()
        //        .HasForeignKey(e => e.CompanyId)
        //        .OnDelete(DeleteBehavior.SetNull);
        //});

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasOne(c => c.User)
            .WithMany(c => c.ScheduledJobs)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(c => c.JobKey);

            entity.HasIndex(c => new { c.JobGroup, c.JobKey }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
