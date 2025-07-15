using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.WorkAssignments;

namespace SWSA.MvcPortal.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }

    #region Client
    internal DbSet<SdnBhdClient> SdnBhdClients { get; set; }
    internal DbSet<LLPClient> LLPClients { get; set; }
    internal DbSet<EnterpriseClient> EnterpriseClients { get; set; }
    internal DbSet<IndividualClient> IndividualClients { get; set; }
    internal DbSet<ClientWorkAllocation> ClientWorkAllocations { get; set; }
    #endregion

    internal DbSet<CommunicationContact> CommunicationContacts { get; set; }
    internal DbSet<OfficialContact> OfficialContacts { get; set; }
    internal DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }
    internal DbSet<CompanyOwner> CompanyOwners { get; set; }
    internal DbSet<DocumentRecord> DocumentRecords { get; set; }

    #region Work Assignment
    internal DbSet<WorkAssignment> WorkAssignments { get; set; }
    internal DbSet<WorkProgress> WorkProgresses { get; set; }
    internal DbSet<WorkAssignmentUserMapping> WorkAssignmentUserMappings { get; set; }
    internal DbSet<AnnualReturnWorkAssignment> AnnualReturnWorkAssignments { get; set; }
    internal DbSet<AuditWorkAssignment> AuditWorkAssignments { get; set; }
    internal DbSet<LLPWorkAssignment> LLPWorkAssignments { get; set; }
    internal DbSet<StrikeOffWorkAssignment> StrikeOffWorkAssignments { get; set; }
    #endregion

    internal DbSet<SystemNotificationLog> SystemNotificationLogs { get; set; }
    internal DbSet<SystemAuditLog> SystemAuditLogs { get; set; }
    internal DbSet<ScheduledJob> ScheduledJobs { get; set; }
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

            entity.HasMany(c => c.SystemAuditLogs).WithOne(c => c.PerformedByUser).HasForeignKey(c => c.PerformedByUserId).OnDelete(DeleteBehavior.SetNull);

        });

        modelBuilder.Entity<SdnBhdClient>()
            .ToTable("SdnBhdClients");

        modelBuilder.Entity<LLPClient>()
            .ToTable("LLPClients");

        modelBuilder.Entity<EnterpriseClient>()
            .ToTable("EnterpriseClients");

        modelBuilder.Entity<IndividualClient>()
            .ToTable("IndividualClients");

        modelBuilder.Entity<CommunicationContact>(entity => { });

        modelBuilder.Entity<WorkAssignment>(entity =>
        {
            entity.HasOne(x => x.Progress)
            .WithOne(p => p.WorkAssignment)
            .HasForeignKey<WorkProgress>(p => p.WorkAssignmentId);
        });

        //Add new Assignment at here
        modelBuilder.Entity<AnnualReturnWorkAssignment>()
            .ToTable("AnnualReturnWorkAssignments");

        modelBuilder.Entity<AuditWorkAssignment>()
            .ToTable("AuditWorkAssignments");

        modelBuilder.Entity<LLPWorkAssignment>()
            .ToTable("LLPWorkAssignments");

        modelBuilder.Entity<StrikeOffWorkAssignment>()
            .ToTable("StrikeOffWorkAssignments");

        modelBuilder.Entity<WorkAssignmentUserMapping>(entity =>
        {
            entity.HasIndex(entity => new { entity.WorkAssignmentId, entity.UserId }).IsUnique();
        });

        modelBuilder.Entity<SystemNotificationLog>(entity =>
        {
            entity.HasIndex(c => new { c.CreatedAt, c.Channel });
            entity.HasIndex(c => new { c.CreatedAt, c.TemplateCode });
        });

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
