using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Company> Companies { get; set; }
    internal DbSet<CompanyCommunicationContact> CompanyCommunicationContact { get; set; }
    internal DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }
    internal DbSet<CompanyOfficialContact> CompanyOfficialContacts { get; set; }
    internal DbSet<CompanyOwner> CompanyOwners { get; set; }
    internal DbSet<CompanyWorkAssignment> CompanyWorkAssignments { get; set; }
    internal DbSet<CompanyWorkProgress> CompanyWorkProgresses { get; set; }
    internal DbSet<WorkAssignmentAuditMonth> WorkAssignmentAuditMonths { get; set; }
    internal DbSet<WorkAssignmentAccountMonth> WorkAssignmentAccountMonths { get; set; }
    internal DbSet<WorkAssignmentUserMapping> WorkAssignmentUserMappings { get; set; }
    internal DbSet<CompanyComplianceDate> CompanyComplianceDates { get; set; }
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

        modelBuilder.Entity<UserCompanyDepartment>(entity =>
        {
            entity
             .HasOne(ucd => ucd.User)
             .WithMany(u => u.CompanyDepartments)
             .HasForeignKey(ucd => ucd.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasMany(c => c.CommunicationContacts)
            .WithOne(cc => cc.Company)
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict); //When deleting a company, please perform delete all contact before delete company;

            entity.HasOne(c => c.ComplianceDate)
            .WithOne(ccd => ccd.Company)
            .HasForeignKey<CompanyComplianceDate>(ccd => ccd.CompanyId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete, enforce manual cleanup

        });

        modelBuilder.Entity<CompanyCommunicationContact>(entity => { });

        modelBuilder.Entity<CompanyComplianceDate>()
            .HasIndex(c => new { c.CompanyId });

        modelBuilder.Entity<CompanyWorkAssignment>(entity =>
        {
            entity.HasOne(x => x.Progress)
            .WithOne(p => p.WorkAssignment)
            .HasForeignKey<CompanyWorkProgress>(p => p.WorkAssignmentId);
        });

        modelBuilder.Entity<WorkAssignmentUserMapping>(entity =>
        {
            entity.HasIndex(entity => new { entity.WorkAssignmentId, entity.UserId, entity.Department }).IsUnique();
        });


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
