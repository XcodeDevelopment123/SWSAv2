using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Backlogs;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.Contacts;
using SWSA.MvcPortal.Entities.Systems;
using SWSA.MvcPortal.Entities.Templates;
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

    #region Template
    internal DbSet<SecDeptTaskTemplate> SecDeptTaskTemplates { get; set; }
    internal DbSet<SecStrikeOffTemplate> SecStrikeOffTemplates { get; set; }
    internal DbSet<TaxStrikeOffTemplate> TaxStrikeOffTemplates { get; set; }
    internal DbSet<AuditTemplate> AuditTemplates { get; set; }
    internal DbSet<AEXTemplate> AEXTemplates { get; set; }
    #endregion

    #region BackLogs
    internal DbSet<AuditBacklogSchedule> AuditBacklogSchedules { get; set; }
    internal DbSet<AEXBacklog> AEXBcklogs { get; set; }
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

        // 添加 decimal 精度配置
        modelBuilder.Entity<AEXTemplate>(entity =>
        {
            entity.Property(e => e.AuditFee).HasPrecision(18, 2);
            entity.Property(e => e.Profit).HasPrecision(18, 2);
            entity.Property(e => e.Revenue).HasPrecision(18, 2);
        });

        modelBuilder.Entity<AuditTemplate>(entity =>
        {
            entity.Property(e => e.AuditFee).HasPrecision(18, 2);
            entity.Property(e => e.Profit).HasPrecision(18, 2);
            entity.Property(e => e.Revenue).HasPrecision(18, 2);
        });

        modelBuilder.Entity<SecStrikeOffTemplate>(entity =>
        {
            entity.Property(e => e.PenaltiesAmount).HasPrecision(18, 2);
            entity.Property(e => e.RevisedPenaltiesAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<TaxStrikeOffTemplate>(entity =>
        {
            entity.Property(e => e.IRBPenaltiesAmount).HasPrecision(18, 2);
            entity.Property(e => e.InvoiceAmount).HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
    }
}