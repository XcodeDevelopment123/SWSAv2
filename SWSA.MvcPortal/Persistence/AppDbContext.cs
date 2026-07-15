using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Entities.Backlogs;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Entities.Contacts;
using SWSA.MvcPortal.Entities.Models;
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

    #region databasefirst
    public virtual DbSet<A31a> A31A { get; set; }

    public virtual DbSet<A31b> A31B { get; set; }

    public virtual DbSet<A32a> A32A { get; set; }

    public virtual DbSet<A32b> A32B { get; set; }

    public virtual DbSet<A33a> A33A { get; set; }

    public virtual DbSet<A33b> A33B { get; set; }

    public virtual DbSet<Aex12> AEX12 { get; set; }

    public virtual DbSet<Aex41> AEX41 { get; set; }

    public virtual DbSet<Aex42> AEX42 { get; set; }

    public virtual DbSet<Aex51> AEX51 { get; set; }

    public virtual DbSet<Aex52> AEX52 { get; set; }

    public virtual DbSet<At11> AT11 { get; set; }

    public virtual DbSet<At21> AT21 { get; set; }

    public virtual DbSet<At22> AT22 { get; set; }

    public virtual DbSet<At31> AT31 { get; set; }

    public virtual DbSet<At32> AT32 { get; set; }

    public virtual DbSet<At33> AT33 { get; set; }

    public virtual DbSet<At34> AT34 { get; set; }

    public virtual DbSet<B11> B11 { get; set; }

    public virtual DbSet<B2> B2 { get; set; }

    public virtual DbSet<B31> B31 { get; set; }

    public virtual DbSet<B32> B32 { get; set; }

    public virtual DbSet<B34> B34 { get; set; }

    public virtual DbSet<B35> B35 { get; set; }

    public virtual DbSet<B36> B36 { get; set; }

    public virtual DbSet<Bp21> BP21 { get; set; }

    public virtual DbSet<Bp22> BP22 { get; set; }

    public virtual DbSet<Bp23> BP23 { get; set; }

    public virtual DbSet<Bp24> BP24 { get; set; }

    public virtual DbSet<Bp25> BP25 { get; set; }

    public virtual DbSet<Bp26> BP26 { get; set; }

    public virtual DbSet<Bp31> BP31 { get; set; }

    public virtual DbSet<Bp32> BP32 { get; set; }

    public virtual DbSet<Bp33> BP33 { get; set; }

    public virtual DbSet<Bp34> BP34 { get; set; }

    public virtual DbSet<FormC> FormC { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<S13a> S13A { get; set; }

    public virtual DbSet<S13b> S13B { get; set; }

    public virtual DbSet<S14a> S14A { get; set; }

    public virtual DbSet<S14b> S14B { get; set; }

    public virtual DbSet<S15> S15 { get; set; }

    public virtual DbSet<S16> S16 { get; set; }

    public virtual DbSet<Tx1> TX1 { get; set; }

    public virtual DbSet<Tx1b> TX1B { get; set; }

    public virtual DbSet<Tx2> TX2 { get; set; }

    public virtual DbSet<Tx3> TX3 { get; set; }

    public virtual DbSet<Tx4> TX4 { get; set; }

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