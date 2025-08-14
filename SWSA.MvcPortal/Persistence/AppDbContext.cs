using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.Contacts;
using SWSA.MvcPortal.Entities.Reminders;
using SWSA.MvcPortal.Entities.SecretaryDept;
using SWSA.MvcPortal.Entities.Systems;
namespace SWSA.MvcPortal.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<User> Users { get; set; }

    #region Client
    internal DbSet<BaseClient> Clients { get; set; }
    internal DbSet<SdnBhdClient> SdnBhdClients { get; set; }
    internal DbSet<LLPClient> LLPClients { get; set; }
    internal DbSet<EnterpriseClient> EnterpriseClients { get; set; }
    internal DbSet<IndividualClient> IndividualClients { get; set; }
    #endregion

    internal DbSet<CommunicationContact> CommunicationContacts { get; set; }
    internal DbSet<OfficialContact> OfficialContacts { get; set; }
    internal DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }
    internal DbSet<CompanyOwner> CompanyOwners { get; set; }
    internal DbSet<DocumentRecord> DocumentRecords { get; set; }
    internal DbSet<SystemNotificationLog> SystemNotificationLogs { get; set; }
    internal DbSet<SystemAuditLog> SystemAuditLogs { get; set; }
    internal DbSet<ScheduledJob> ScheduledJobs { get; set; }
    internal DbSet<MsicCode> MsicCodes { get; set; }

    #region  Reminders
    internal DbSet<Reminder> Reminders { get; set; }
    #endregion

    #region Template
    internal DbSet<SecDeptTaskTemplate> SecDeptTaskTemplates { get; set; }
    internal DbSet<SecStrikeOffTemplate> SecStrikeOffTemplates { get; set; }

    #endregion

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

        modelBuilder.Entity<BaseClient>().UseTptMappingStrategy();
        modelBuilder.Entity<IndividualClient>().ToTable("IndividualClients");
        modelBuilder.Entity<BaseCompany>().ToTable("BaseCompanies");
        modelBuilder.Entity<SdnBhdClient>().ToTable("SdnBhdClients");
        modelBuilder.Entity<LLPClient>().ToTable("LLPClients");
        modelBuilder.Entity<EnterpriseClient>().ToTable("EnterpriseClients");

        modelBuilder.Entity<CommunicationContact>(entity => { });

        modelBuilder.Entity<SystemNotificationLog>(entity =>
        {
            entity.HasIndex(c => new { c.CreatedAt, c.Channel });
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

        // Reminder Configuration
        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasIndex(r => new { r.TargetAt, r.Status });
            entity.HasIndex(r => r.ScheduledWorkAllocationId);

            entity.Property(r => r.Title).HasMaxLength(200).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
