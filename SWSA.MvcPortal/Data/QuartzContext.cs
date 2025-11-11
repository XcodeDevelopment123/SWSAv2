using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Data.Models;

namespace SWSA.MvcPortal.Data;

public partial class QuartzContext : DbContext
{
    public QuartzContext()
    {
    }

    public QuartzContext(DbContextOptions<QuartzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<A31a> A31as { get; set; }

    public virtual DbSet<A31b> A31bs { get; set; }

    public virtual DbSet<A32a> A32as { get; set; }

    public virtual DbSet<A32b> A32bs { get; set; }

    public virtual DbSet<A33a> A33as { get; set; }

    public virtual DbSet<A33b> A33bs { get; set; }

    public virtual DbSet<Aex41> Aex41s { get; set; }

    public virtual DbSet<Aex51> Aex51s { get; set; }

    public virtual DbSet<Aex52> Aex52s { get; set; }

    public virtual DbSet<Aexbcklog> Aexbcklogs { get; set; }

    public virtual DbSet<Aextemplate> Aextemplates { get; set; }

    public virtual DbSet<At21> At21s { get; set; }

    public virtual DbSet<At31> At31s { get; set; }

    public virtual DbSet<At32> At32s { get; set; }

    public virtual DbSet<At33> At33s { get; set; }

    public virtual DbSet<At34> At34s { get; set; }

    public virtual DbSet<AuditBacklogSchedule> AuditBacklogSchedules { get; set; }

    public virtual DbSet<AuditTemplate> AuditTemplates { get; set; }

    public virtual DbSet<B11> B11s { get; set; }

    public virtual DbSet<B2> B2s { get; set; }

    public virtual DbSet<B31> B31s { get; set; }

    public virtual DbSet<B32> B32s { get; set; }

    public virtual DbSet<B34> B34s { get; set; }

    public virtual DbSet<B35> B35s { get; set; }

    public virtual DbSet<B36> B36s { get; set; }

    public virtual DbSet<BaseCompany> BaseCompanies { get; set; }

    public virtual DbSet<Bp21> Bp21s { get; set; }

    public virtual DbSet<Bp22> Bp22s { get; set; }

    public virtual DbSet<Bp23> Bp23s { get; set; }

    public virtual DbSet<Bp24> Bp24s { get; set; }

    public virtual DbSet<Bp25> Bp25s { get; set; }

    public virtual DbSet<Bp26> Bp26s { get; set; }

    public virtual DbSet<Bp31> Bp31s { get; set; }

    public virtual DbSet<Bp32> Bp32s { get; set; }

    public virtual DbSet<Bp33> Bp33s { get; set; }

    public virtual DbSet<Bp34> Bp34s { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<CommunicationContact> CommunicationContacts { get; set; }

    public virtual DbSet<CompanyMsicCode> CompanyMsicCodes { get; set; }

    public virtual DbSet<CompanyOwner> CompanyOwners { get; set; }

    public virtual DbSet<DocumentRecord> DocumentRecords { get; set; }

    public virtual DbSet<EnterpriseClient> EnterpriseClients { get; set; }

    public virtual DbSet<IndividualClient> IndividualClients { get; set; }

    public virtual DbSet<Llpclient> Llpclients { get; set; }

    public virtual DbSet<MsicCode> MsicCodes { get; set; }

    public virtual DbSet<OfficialContact> OfficialContacts { get; set; }

    public virtual DbSet<S13a> S13as { get; set; }

    public virtual DbSet<S13b> S13bs { get; set; }

    public virtual DbSet<S14a> S14as { get; set; }

    public virtual DbSet<S14b> S14bs { get; set; }

    public virtual DbSet<S15> S15s { get; set; }

    public virtual DbSet<S16> S16s { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJobs { get; set; }

    public virtual DbSet<SdnBhdClient> SdnBhdClients { get; set; }

    public virtual DbSet<SecDeptTaskTemplate> SecDeptTaskTemplates { get; set; }

    public virtual DbSet<SecStrikeOffTemplate> SecStrikeOffTemplates { get; set; }

    public virtual DbSet<SystemAuditLog> SystemAuditLogs { get; set; }

    public virtual DbSet<SystemNotificationLog> SystemNotificationLogs { get; set; }

    public virtual DbSet<TaxStrikeOffTemplate> TaxStrikeOffTemplates { get; set; }

    public virtual DbSet<Tx1> Tx1s { get; set; }

    public virtual DbSet<Tx1b> Tx1bs { get; set; }

    public virtual DbSet<Tx2> Tx2s { get; set; }

    public virtual DbSet<Tx3> Tx3s { get; set; }

    public virtual DbSet<Tx4> Tx4s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SY\\SQLEXPRESS;Database=Quartz;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<A31a>(entity =>
        {
            entity.ToTable("A31A");

            entity.Property(e => e.ByWhoam2).HasMaxLength(50);
            entity.Property(e => e.ByWhom).HasMaxLength(50);
            entity.Property(e => e.Client).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(50);
            entity.Property(e => e.DateReceived).HasMaxLength(50);
            entity.Property(e => e.DateSendToAd)
                .HasMaxLength(50)
                .HasColumnName("DateSendToAD");
            entity.Property(e => e.NoOfBagBox).HasMaxLength(50);
            entity.Property(e => e.NoOfBoxBag).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Remark2).HasMaxLength(50);
            entity.Property(e => e.UploadLetter).HasMaxLength(50);
            entity.Property(e => e.UploadLetter2).HasMaxLength(50);
            entity.Property(e => e.YearEnded).HasMaxLength(50);
        });

        modelBuilder.Entity<A31b>(entity =>
        {
            entity.ToTable("A31B");

            entity.Property(e => e.ByWhom).HasMaxLength(500);
            entity.Property(e => e.ByWhom2).HasMaxLength(500);
            entity.Property(e => e.Clients).HasMaxLength(500);
            entity.Property(e => e.CoStatus).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(50);
            entity.Property(e => e.DateDocFr).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.NoOfBoxBag)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NoOfbox).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.Remark2)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UploadLetter).HasMaxLength(500);
            entity.Property(e => e.UploadLetter2).HasMaxLength(500);
            entity.Property(e => e.YearEnded).HasMaxLength(500);
        });

        modelBuilder.Entity<A32a>(entity =>
        {
            entity.ToTable("A32A");

            entity.Property(e => e.BriefDescritions).HasMaxLength(500);
            entity.Property(e => e.CaseNo).HasMaxLength(500);
            entity.Property(e => e.Client).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.Details).HasMaxLength(500);
            entity.Property(e => e.DoneOn).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.TypeIncoming).HasMaxLength(500);
            entity.Property(e => e.YearAssessment).HasMaxLength(500);
        });

        modelBuilder.Entity<A32b>(entity =>
        {
            entity.ToTable("A32B");

            entity.Property(e => e.CaseNo).HasMaxLength(500);
            entity.Property(e => e.Client).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(500);
            entity.Property(e => e.DateIrbemailLetter)
                .HasMaxLength(500)
                .HasColumnName("DateIRBemailLetter");
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DetailsCorrepondence).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OfficerInCharge).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.TelExtension).HasMaxLength(500);
            entity.Property(e => e.YearAssessment).HasMaxLength(500);
        });

        modelBuilder.Entity<A33a>(entity =>
        {
            entity.ToTable("A33A");

            entity.Property(e => e.BriefDescritions).HasMaxLength(500);
            entity.Property(e => e.CaseNo).HasMaxLength(500);
            entity.Property(e => e.Client).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.Details).HasMaxLength(500);
            entity.Property(e => e.DoneOn).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.TypeIncoming).HasMaxLength(500);
            entity.Property(e => e.YearAssessment).HasMaxLength(500);
        });

        modelBuilder.Entity<A33b>(entity =>
        {
            entity.ToTable("A33B");

            entity.Property(e => e.CaseNo).HasMaxLength(500);
            entity.Property(e => e.Client).HasMaxLength(500);
            entity.Property(e => e.Date).HasMaxLength(500);
            entity.Property(e => e.DateIrbemailLetter)
                .HasMaxLength(500)
                .HasColumnName("DateIRBemailLetter");
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DetailsCorrepondence).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OfficerInchrage).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.TelExtension).HasMaxLength(500);
            entity.Property(e => e.YearAssessment).HasMaxLength(500);
        });

        modelBuilder.Entity<Aex41>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AEX41");

            entity.Property(e => e.AcctngWk).HasMaxLength(500);
            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.AuditedAccDueDate).HasMaxLength(500);
            entity.Property(e => e.CoSec).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.EstNetProfit).HasMaxLength(500);
            entity.Property(e => e.EstRev).HasMaxLength(500);
            entity.Property(e => e.First18mthsdue).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.MovetoActiveSch).HasMaxLength(500);
            entity.Property(e => e.MovetoBacklog).HasMaxLength(500);
            entity.Property(e => e.QuartertoAudit).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.Team).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yeattodo).HasMaxLength(500);
        });

        modelBuilder.Entity<Aex51>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AEX51");

            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.AuditFee).HasMaxLength(500);
            entity.Property(e => e.Binded).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateBilled).HasMaxLength(500);
            entity.Property(e => e.DatePassToSecDept).HasMaxLength(500);
            entity.Property(e => e.DateReceivedBack).HasMaxLength(500);
            entity.Property(e => e.DateReceivedfrKk)
                .HasMaxLength(500)
                .HasColumnName("DateReceivedfrKK");
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateSentClient).HasMaxLength(500);
            entity.Property(e => e.DateSenttoKk)
                .HasMaxLength(500)
                .HasColumnName("DateSenttoKK");
            entity.Property(e => e.DespatachDateClient).HasMaxLength(500);
            entity.Property(e => e.DocInwardsDate).HasMaxLength(500);
            entity.Property(e => e.DonePercent).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.First18mthDue).HasMaxLength(500);
            entity.Property(e => e.PasstoTaxDept).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Profit).HasMaxLength(500);
            entity.Property(e => e.Quartertodo).HasMaxLength(500);
            entity.Property(e => e.ResultOverUnder).HasMaxLength(500);
            entity.Property(e => e.Revenue).HasMaxLength(500);
            entity.Property(e => e.ReviewResult).HasMaxLength(500);
            entity.Property(e => e.SsmdueDate)
                .HasMaxLength(500)
                .HasColumnName("SSMdueDate");
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(500);
            entity.Property(e => e.TaxDueDate).HasMaxLength(500);
            entity.Property(e => e.WhomeetClientDate).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Aex52>(entity =>
        {
            entity.ToTable("AEX52");

            entity.Property(e => e.AccSetup).HasMaxLength(500);
            entity.Property(e => e.AccSummary).HasMaxLength(500);
            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.AuditCompletion).HasMaxLength(500);
            entity.Property(e => e.AuditExecution).HasMaxLength(500);
            entity.Property(e => e.AuditFee).HasMaxLength(500);
            entity.Property(e => e.AuditPlanning).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateBilled).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.Final).HasMaxLength(500);
            entity.Property(e => e.KkdateSent)
                .HasMaxLength(500)
                .HasColumnName("KKDateSent");
            entity.Property(e => e.KkendDate)
                .HasMaxLength(500)
                .HasColumnName("KKEndDate");
            entity.Property(e => e.KkresultOverUnder)
                .HasMaxLength(500)
                .HasColumnName("KKResultOverUnder");
            entity.Property(e => e.NoofDays).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Quartertodo).HasMaxLength(500);
            entity.Property(e => e.ResultOverUnder).HasMaxLength(500);
            entity.Property(e => e.ReviewDateSent).HasMaxLength(500);
            entity.Property(e => e.ReviewEndDate).HasMaxLength(500);
            entity.Property(e => e.ReviewResultOverUnder).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(500);
            entity.Property(e => e.TotalPercent).HasMaxLength(500);
            entity.Property(e => e.Yeartodo).HasMaxLength(500);
        });

        modelBuilder.Entity<Aexbcklog>(entity =>
        {
            entity.ToTable("AEXBcklogs");

            entity.HasIndex(e => e.ClientId, "IX_AEXBcklogs_ClientId");

            entity.HasOne(d => d.Client).WithMany(p => p.Aexbcklogs).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<Aextemplate>(entity =>
        {
            entity.ToTable("AEXTemplates");

            entity.HasIndex(e => e.ClientId, "IX_AEXTemplates_ClientId");

            entity.HasIndex(e => e.PersonInChargeId, "IX_AEXTemplates_PersonInChargeId");

            entity.Property(e => e.AuditFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AuditWipresult).HasColumnName("AuditWIPResult");
            entity.Property(e => e.Profit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SecSsmdueDate).HasColumnName("SecSSMDueDate");

            entity.HasOne(d => d.Client).WithMany(p => p.Aextemplates).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.PersonInCharge).WithMany(p => p.Aextemplates).HasForeignKey(d => d.PersonInChargeId);
        });

        modelBuilder.Entity<At21>(entity =>
        {
            entity.ToTable("AT21");

            entity.Property(e => e.AcctngWk).HasMaxLength(500);
            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.AfsdueDate)
                .HasMaxLength(500)
                .HasColumnName("AFSdueDate");
            entity.Property(e => e.AuditStaff).HasMaxLength(500);
            entity.Property(e => e.AuditStatus).HasMaxLength(500);
            entity.Property(e => e.CoSec).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.EstRev).HasMaxLength(500);
            entity.Property(e => e.First18mthdue).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.MovetoAex)
                .HasMaxLength(500)
                .HasColumnName("MovetoAEX");
            entity.Property(e => e.MovetoBacklog).HasMaxLength(500);
            entity.Property(e => e.QuartertoAudit).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<At31>(entity =>
        {
            entity.ToTable("AT31");

            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.AuditFee).HasMaxLength(500);
            entity.Property(e => e.Binded).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateBilled).HasMaxLength(500);
            entity.Property(e => e.DatePasstoSecDept).HasMaxLength(500);
            entity.Property(e => e.DateReceiveBack).HasMaxLength(500);
            entity.Property(e => e.DateReceiveFromKk)
                .HasMaxLength(500)
                .HasColumnName("DateReceiveFromKK");
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateSentToKk)
                .HasMaxLength(500)
                .HasColumnName("DateSentToKK");
            entity.Property(e => e.DateSenttoClient).HasMaxLength(500);
            entity.Property(e => e.DaysDone).HasMaxLength(500);
            entity.Property(e => e.DespatchDateToClient).HasMaxLength(500);
            entity.Property(e => e.DocInwardsDate).HasMaxLength(500);
            entity.Property(e => e.DonePercent).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.MthDue).HasMaxLength(500);
            entity.Property(e => e.PasstoDept).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Profit).HasMaxLength(500);
            entity.Property(e => e.QuartertoDo).HasMaxLength(500);
            entity.Property(e => e.Revenue).HasMaxLength(500);
            entity.Property(e => e.ReviewResultofDays).HasMaxLength(500);
            entity.Property(e => e.SsmdueDate)
                .HasMaxLength(500)
                .HasColumnName("SSMdueDate");
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(500);
            entity.Property(e => e.TaxDueDate).HasMaxLength(500);
            entity.Property(e => e.WhoMeetClientDate).HasMaxLength(500);
            entity.Property(e => e.YetoDo)
                .HasMaxLength(500)
                .HasColumnName("YEtoDo");
        });

        modelBuilder.Entity<At32>(entity =>
        {
            entity.ToTable("AT32");

            entity.Property(e => e.AccSetup).HasMaxLength(500);
            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.Assummary).HasMaxLength(500);
            entity.Property(e => e.AuditCompleion).HasMaxLength(500);
            entity.Property(e => e.AuditExecution).HasMaxLength(500);
            entity.Property(e => e.AuditFee).HasMaxLength(500);
            entity.Property(e => e.AuditPlanning).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateBilled).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.FinalResultOverUnder).HasMaxLength(500);
            entity.Property(e => e.KkdateSent)
                .HasMaxLength(500)
                .HasColumnName("KKdateSent");
            entity.Property(e => e.KkendDate)
                .HasMaxLength(500)
                .HasColumnName("KKendDate");
            entity.Property(e => e.KkofDate)
                .HasMaxLength(500)
                .HasColumnName("KKofDate");
            entity.Property(e => e.NoOfDays).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Quartertodo).HasMaxLength(500);
            entity.Property(e => e.ReviewDateSent).HasMaxLength(500);
            entity.Property(e => e.ReviewEndDate).HasMaxLength(500);
            entity.Property(e => e.ReviewOfDays).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(500);
            entity.Property(e => e.TotalFieldwkDays).HasMaxLength(500);
            entity.Property(e => e.TotalPercent).HasMaxLength(500);
            entity.Property(e => e.TotalReviewDays).HasMaxLength(500);
            entity.Property(e => e.YeartoDo).HasMaxLength(500);
        });

        modelBuilder.Entity<At33>(entity =>
        {
            entity.ToTable("AT33");

            entity.Property(e => e.Active).HasMaxLength(500);
            entity.Property(e => e.Aex)
                .HasMaxLength(500)
                .HasColumnName("AEX");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceivedAfs)
                .HasMaxLength(500)
                .HasColumnName("DateReceivedAFS");
            entity.Property(e => e.DateReviewed).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateSentToKk)
                .HasMaxLength(500)
                .HasColumnName("DateSentToKK");
            entity.Property(e => e.DateofAfs)
                .HasMaxLength(500)
                .HasColumnName("DateofAFS");
            entity.Property(e => e.DateofDirectorsRept).HasMaxLength(500);
            entity.Property(e => e.Mbrsgenerated)
                .HasMaxLength(500)
                .HasColumnName("MBRSgenerated");
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<At34>(entity =>
        {
            entity.ToTable("AT34");

            entity.Property(e => e.Active).HasMaxLength(500);
            entity.Property(e => e.Aex)
                .HasMaxLength(500)
                .HasColumnName("AEX");
            entity.Property(e => e.CommofOathsDate).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.First18mthDate).HasMaxLength(500);
            entity.Property(e => e.FlwUpDates).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<AuditBacklogSchedule>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_AuditBacklogSchedules_ClientId");

            entity.HasOne(d => d.Client).WithMany(p => p.AuditBacklogSchedules).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<AuditTemplate>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_AuditTemplates_ClientId");

            entity.HasIndex(e => e.PersonInChargeId, "IX_AuditTemplates_PersonInChargeId");

            entity.Property(e => e.AuditFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AuditWipresult).HasColumnName("AuditWIPResult");
            entity.Property(e => e.Profit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SecSsmdueDate).HasColumnName("SecSSMDueDate");

            entity.HasOne(d => d.Client).WithMany(p => p.AuditTemplates).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.PersonInCharge).WithMany(p => p.AuditTemplates).HasForeignKey(d => d.PersonInChargeId);
        });

        modelBuilder.Entity<B11>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_B1.1");

            entity.ToTable("B11");

            entity.Property(e => e.CirculationAfsdueDate)
                .HasMaxLength(500)
                .HasColumnName("CirculationAFSDueDate");
            entity.Property(e => e.Company).HasMaxLength(500);
            entity.Property(e => e.CompanyNo).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.EmailSend).HasMaxLength(500);
            entity.Property(e => e.File).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IncorporationDate).HasMaxLength(500);
            entity.Property(e => e.ReminderDate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.YmdueDate)
                .HasMaxLength(500)
                .HasColumnName("YMDueDate");
        });

        modelBuilder.Entity<B2>(entity =>
        {
            entity.ToTable("B2");

            entity.Property(e => e.ActiveStatus).HasMaxLength(500);
            entity.Property(e => e.Aexstatus)
                .HasMaxLength(500)
                .HasColumnName("AEXstatus");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateReceived2).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSend).HasMaxLength(500);
            entity.Property(e => e.DateSend2).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateText).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Ssm18mthDue)
                .HasMaxLength(500)
                .HasColumnName("SSM18MthDue");
            entity.Property(e => e.SsmTax)
                .HasMaxLength(500)
                .HasColumnName("SSM_TAX");
            entity.Property(e => e.TargetedCall).HasMaxLength(500);
            entity.Property(e => e.TargetedDate).HasMaxLength(500);
            entity.Property(e => e.TargetedFinalText).HasMaxLength(500);
            entity.Property(e => e.TargetedReminder).HasMaxLength(500);
            entity.Property(e => e.TargetedSendDate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<B31>(entity =>
        {
            entity.ToTable("B31");

            entity.Property(e => e.ActiveStatus).HasMaxLength(500);
            entity.Property(e => e.Aexstatus)
                .HasMaxLength(500)
                .HasColumnName("AEXstatus");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateText).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Ssm18mthDue)
                .HasMaxLength(500)
                .HasColumnName("SSM18MthDue");
            entity.Property(e => e.SsmTax)
                .HasMaxLength(500)
                .HasColumnName("SSM_TAX");
            entity.Property(e => e.TCall)
                .HasMaxLength(500)
                .HasColumnName("T_Call");
            entity.Property(e => e.TDate)
                .HasMaxLength(500)
                .HasColumnName("T_Date");
            entity.Property(e => e.TFinalText)
                .HasMaxLength(500)
                .HasColumnName("T_FinalText");
            entity.Property(e => e.TStartAccWk)
                .HasMaxLength(500)
                .HasColumnName("T_startAccWk");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<B32>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("B32");

            entity.Property(e => e.ActiveStatus).HasMaxLength(500);
            entity.Property(e => e.Aexstatus)
                .HasMaxLength(500)
                .HasColumnName("AEXstatus");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateText).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Ssm18mthDue)
                .HasMaxLength(500)
                .HasColumnName("SSM18MthDue");
            entity.Property(e => e.SsmTax)
                .HasMaxLength(500)
                .HasColumnName("SSM_TAX");
            entity.Property(e => e.TCall)
                .HasMaxLength(500)
                .HasColumnName("T_Call");
            entity.Property(e => e.TDate)
                .HasMaxLength(500)
                .HasColumnName("T_Date");
            entity.Property(e => e.TFinalText)
                .HasMaxLength(500)
                .HasColumnName("T_FinalText");
            entity.Property(e => e.TStartAccWk)
                .HasMaxLength(500)
                .HasColumnName("T_startAccWk");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<B34>(entity =>
        {
            entity.ToTable("B34");

            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.FullSet).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.Reveiw).HasMaxLength(500);
            entity.Property(e => e.SingleEntry).HasMaxLength(500);
            entity.Property(e => e.TCall)
                .HasMaxLength(500)
                .HasColumnName("T_Call");
            entity.Property(e => e.TDate)
                .HasMaxLength(500)
                .HasColumnName("T_Date");
            entity.Property(e => e.TFinalText)
                .HasMaxLength(500)
                .HasColumnName("T_FinalText");
            entity.Property(e => e.TStartAccWk)
                .HasMaxLength(500)
                .HasColumnName("T_startAccWk");
            entity.Property(e => e.TaxOnly).HasMaxLength(500);
        });

        modelBuilder.Entity<B35>(entity =>
        {
            entity.ToTable("B35");

            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateText).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.TCall)
                .HasMaxLength(500)
                .HasColumnName("T_call");
            entity.Property(e => e.TDate)
                .HasMaxLength(500)
                .HasColumnName("T_Date");
            entity.Property(e => e.TFinalText)
                .HasMaxLength(500)
                .HasColumnName("T_finalText");
            entity.Property(e => e.TStartWk)
                .HasMaxLength(500)
                .HasColumnName("T_startWk");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<B36>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("B36");

            entity.Property(e => e.DateReceived).HasMaxLength(500);
            entity.Property(e => e.DateRemind).HasMaxLength(500);
            entity.Property(e => e.DateSent).HasMaxLength(500);
            entity.Property(e => e.DateText).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IndividualTaxPayer).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Pic)
                .HasMaxLength(500)
                .HasColumnName("PIC");
            entity.Property(e => e.TCall)
                .HasMaxLength(500)
                .HasColumnName("T_Call");
            entity.Property(e => e.TDate)
                .HasMaxLength(500)
                .HasColumnName("T_Date");
            entity.Property(e => e.TFinalText)
                .HasMaxLength(500)
                .HasColumnName("T_FinalText");
            entity.Property(e => e.TStartWk)
                .HasMaxLength(500)
                .HasColumnName("T_startWk");
        });

        modelBuilder.Entity<BaseCompany>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.BaseCompany).HasForeignKey<BaseCompany>(d => d.Id);
        });

        modelBuilder.Entity<Bp21>(entity =>
        {
            entity.ToTable("BP21");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Co)
                .HasMaxLength(500)
                .HasColumnName("CO");
            entity.Property(e => e.CoStatus).HasMaxLength(500);
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DueDate).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.EstmthToDo)
                .HasMaxLength(500)
                .HasColumnName("ESTmthToDo");
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.ServiceType).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.Tinnumber)
                .HasMaxLength(500)
                .HasColumnName("TINnumber");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp22>(entity =>
        {
            entity.ToTable("BP22");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AddueDate)
                .HasMaxLength(500)
                .HasColumnName("ADdueDate");
            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Co)
                .HasMaxLength(500)
                .HasColumnName("CO");
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.ExtensionDate).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.MthToDo).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.ServicesType).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.Tinnumber)
                .HasMaxLength(500)
                .HasColumnName("TINnumber");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp23>(entity =>
        {
            entity.ToTable("BP23");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Co)
                .HasMaxLength(500)
                .HasColumnName("CO");
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.JobService).HasMaxLength(500);
            entity.Property(e => e.Login).HasMaxLength(500);
            entity.Property(e => e.MthTodo).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.RegistrationDate).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.Tinnumber)
                .HasMaxLength(500)
                .HasColumnName("TINnumber");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.YetoDo)
                .HasMaxLength(500)
                .HasColumnName("YEtoDo");
        });

        modelBuilder.Entity<Bp24>(entity =>
        {
            entity.ToTable("BP24");

            entity.Property(e => e.ActiveCoActivitSize).HasMaxLength(500);
            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.MthTodo).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.ServicesType).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp25>(entity =>
        {
            entity.ToTable("BP25");

            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.JobServices)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Login).HasMaxLength(500);
            entity.Property(e => e.MthTodo).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.TinNumber).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp26>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BP26");

            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.JobServices)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Login).HasMaxLength(500);
            entity.Property(e => e.MthTodo).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Refferal).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.TinNumber).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp31>(entity =>
        {
            entity.ToTable("BP31");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AllocateToWkSch).HasMaxLength(500);
            entity.Property(e => e.Co)
                .HasMaxLength(500)
                .HasColumnName("CO");
            entity.Property(e => e.CoStatus).HasMaxLength(500);
            entity.Property(e => e.Code).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateDocIn).HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Enumber).HasMaxLength(500);
            entity.Property(e => e.EstMthTodo).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.Referral).HasMaxLength(500);
            entity.Property(e => e.ServicesType).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.Tinnumber)
                .HasMaxLength(500)
                .HasColumnName("TINnumber");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.YetoDo)
                .HasMaxLength(500)
                .HasColumnName("YEtoDo");
        });

        modelBuilder.Entity<Bp32>(entity =>
        {
            entity.ToTable("BP32");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AmountRm)
                .HasMaxLength(500)
                .HasColumnName("AmountRM");
            entity.Property(e => e.AmountTaxPay).HasMaxLength(500);
            entity.Property(e => e.CoStatus).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompletedBacklog).HasMaxLength(500);
            entity.Property(e => e.DatePassToAudit).HasMaxLength(500);
            entity.Property(e => e.DateTaxSubmited).HasMaxLength(500);
            entity.Property(e => e.DocDespatchDate).HasMaxLength(500);
            entity.Property(e => e.DocReceivedDate).HasMaxLength(500);
            entity.Property(e => e.DraftFinancialStatement).HasMaxLength(500);
            entity.Property(e => e.DraftTaxCompleted).HasMaxLength(500);
            entity.Property(e => e.EfileDraft)
                .HasMaxLength(500)
                .HasColumnName("EFileDraft");
            entity.Property(e => e.EfileFinal)
                .HasMaxLength(500)
                .HasColumnName("EFileFinal");
            entity.Property(e => e.EfileviaSpc)
                .HasMaxLength(500)
                .HasColumnName("EFileviaSPC");
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.FinalTax).HasMaxLength(500);
            entity.Property(e => e.InvoiceNo).HasMaxLength(500);
            entity.Property(e => e.JobServices).HasMaxLength(500);
            entity.Property(e => e.KeyinToExcel).HasMaxLength(500);
            entity.Property(e => e.MthTodo).HasMaxLength(500);
            entity.Property(e => e.ReviewTax).HasMaxLength(500);
            entity.Property(e => e.ReviewWorkingAcc).HasMaxLength(500);
            entity.Property(e => e.SingleEntry).HasMaxLength(500);
            entity.Property(e => e.SortingFiling).HasMaxLength(500);
            entity.Property(e => e.Staff).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.TaxArdueDate)
                .HasMaxLength(500)
                .HasColumnName("TaxARdueDate");
            entity.Property(e => e.TaxComFinalSignByClient).HasMaxLength(500);
            entity.Property(e => e.TaxComputation).HasMaxLength(500);
            entity.Property(e => e.TimeTaken).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<Bp33>(entity =>
        {
            entity.ToTable("BP33");

            entity.Property(e => e.AmountofTaxPay).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DocDespatchDate).HasMaxLength(500);
            entity.Property(e => e.DraftTaxCompleted).HasMaxLength(500);
            entity.Property(e => e.EfileDraft)
                .HasMaxLength(500)
                .HasColumnName("EFileDraft");
            entity.Property(e => e.EfileFinal)
                .HasMaxLength(500)
                .HasColumnName("EFileFinal");
            entity.Property(e => e.FinalTax).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.InvoicesNo).HasMaxLength(500);
            entity.Property(e => e.Item).HasMaxLength(500);
            entity.Property(e => e.Login).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.ReviewTax).HasMaxLength(500);
            entity.Property(e => e.Spc)
                .HasMaxLength(500)
                .HasColumnName("SPC");
            entity.Property(e => e.TaxComFinalSignByClient).HasMaxLength(500);
            entity.Property(e => e.TaxReferennceNo).HasMaxLength(500);
            entity.Property(e => e.TypeofForm).HasMaxLength(500);
        });

        modelBuilder.Entity<Bp34>(entity =>
        {
            entity.ToTable("BP34");

            entity.Property(e => e.AmountTaxPay).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DocDespatchDate).HasMaxLength(500);
            entity.Property(e => e.DraftTaxCompleted).HasMaxLength(500);
            entity.Property(e => e.EfileDraft)
                .HasMaxLength(500)
                .HasColumnName("EFileDraft");
            entity.Property(e => e.EfileFinal)
                .HasMaxLength(500)
                .HasColumnName("EFileFinal");
            entity.Property(e => e.FinalTax).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.InvoiceNo).HasMaxLength(500);
            entity.Property(e => e.Item).HasMaxLength(500);
            entity.Property(e => e.Login).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.ReviewTax).HasMaxLength(500);
            entity.Property(e => e.Spc)
                .HasMaxLength(500)
                .HasColumnName("SPC");
            entity.Property(e => e.TaxComFinalSignbyClient).HasMaxLength(500);
            entity.Property(e => e.TaxRefferanceNo).HasMaxLength(500);
            entity.Property(e => e.TypeofForm).HasMaxLength(500);
        });

        modelBuilder.Entity<CommunicationContact>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_CommunicationContacts_ClientId");

            entity.HasOne(d => d.Client).WithMany(p => p.CommunicationContacts).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<CompanyMsicCode>(entity =>
        {
            entity.HasIndex(e => e.CompanyId, "IX_CompanyMsicCodes_CompanyId");

            entity.HasIndex(e => e.MsicCodeId, "IX_CompanyMsicCodes_MsicCodeId");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyMsicCodes).HasForeignKey(d => d.CompanyId);

            entity.HasOne(d => d.MsicCode).WithMany(p => p.CompanyMsicCodes).HasForeignKey(d => d.MsicCodeId);
        });

        modelBuilder.Entity<CompanyOwner>(entity =>
        {
            entity.HasIndex(e => e.ClientCompanyId, "IX_CompanyOwners_ClientCompanyId");

            entity.Property(e => e.IcorPassportNumber).HasColumnName("ICOrPassportNumber");
            entity.Property(e => e.NamePerIc).HasColumnName("NamePerIC");
            entity.Property(e => e.RequiresFormBesubmission).HasColumnName("RequiresFormBESubmission");

            entity.HasOne(d => d.ClientCompany).WithMany(p => p.CompanyOwners).HasForeignKey(d => d.ClientCompanyId);
        });

        modelBuilder.Entity<DocumentRecord>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_DocumentRecords_ClientId");

            entity.HasIndex(e => e.HandledByStaffId, "IX_DocumentRecords_HandledByStaffId");

            entity.HasOne(d => d.Client).WithMany(p => p.DocumentRecords).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.HandledByStaff).WithMany(p => p.DocumentRecords).HasForeignKey(d => d.HandledByStaffId);
        });

        modelBuilder.Entity<EnterpriseClient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.EnterpriseClient).HasForeignKey<EnterpriseClient>(d => d.Id);
        });

        modelBuilder.Entity<IndividualClient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IcorPassportNumber).HasColumnName("ICOrPassportNumber");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.IndividualClient).HasForeignKey<IndividualClient>(d => d.Id);
        });

        modelBuilder.Entity<Llpclient>(entity =>
        {
            entity.ToTable("LLPClients");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Llpclient).HasForeignKey<Llpclient>(d => d.Id);
        });

        modelBuilder.Entity<OfficialContact>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_OfficialContacts_ClientId");

            entity.HasOne(d => d.Client).WithMany(p => p.OfficialContacts).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<S13a>(entity =>
        {
            entity.ToTable("S13A");

            entity.Property(e => e.AccmthTodo)
                .HasMaxLength(500)
                .HasColumnName("ACCmthTodo");
            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AfssubmitDate)
                .HasMaxLength(500)
                .HasColumnName("AFSSubmitDate");
            entity.Property(e => e.ArdueDate)
                .HasMaxLength(500)
                .HasColumnName("ARdueDate");
            entity.Property(e => e.ArsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("ARSubmitDate");
            entity.Property(e => e.AuditMthTodo).HasMaxLength(500);
            entity.Property(e => e.Circulation).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.CompanyType).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.Referral).HasMaxLength(500);
            entity.Property(e => e.SecFileNo).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.YetoDo)
                .HasMaxLength(500)
                .HasColumnName("YEtoDo");
            entity.Property(e => e.YrMthDueDate).HasMaxLength(500);
        });

        modelBuilder.Entity<S13b>(entity =>
        {
            entity.ToTable("S13B");

            entity.Property(e => e.AccReady).HasMaxLength(500);
            entity.Property(e => e.AccmthTodo)
                .HasMaxLength(500)
                .HasColumnName("ACCmthTodo");
            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AddueDate)
                .HasMaxLength(500)
                .HasColumnName("ADdueDate");
            entity.Property(e => e.AdsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("ADsubmitDate");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.Grouping).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.Referral).HasMaxLength(500);
            entity.Property(e => e.SecFileNo).HasMaxLength(500);
            entity.Property(e => e.SsmextensionDate)
                .HasMaxLength(500)
                .HasColumnName("SSMextensionDate");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.Yetodo)
                .HasMaxLength(500)
                .HasColumnName("YEtodo");
        });

        modelBuilder.Entity<S14a>(entity =>
        {
            entity.ToTable("S14A");

            entity.Property(e => e.AnniversaryDate).HasMaxLength(500);
            entity.Property(e => e.ArdueDate)
                .HasMaxLength(500)
                .HasColumnName("ARdueDate");
            entity.Property(e => e.ArsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("ARsubmitDate");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyNo).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.DateOfAr)
                .HasMaxLength(500)
                .HasColumnName("DateOfAR");
            entity.Property(e => e.DateReturned).HasMaxLength(500);
            entity.Property(e => e.DateSendtoClient).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.ReminderDate).HasMaxLength(500);
        });

        modelBuilder.Entity<S14b>(entity =>
        {
            entity.ToTable("S14B");

            entity.Property(e => e.CirculationAfsduedate)
                .HasMaxLength(500)
                .HasColumnName("CirculationAFSduedate");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyNo).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.FileNo).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.MbrsreceivedDate)
                .HasMaxLength(500)
                .HasColumnName("MBRSreceivedDate");
            entity.Property(e => e.OntimeLate).HasMaxLength(500);
            entity.Property(e => e.ReasonForLate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
            entity.Property(e => e.YrMthdueDate).HasMaxLength(500);
        });

        modelBuilder.Entity<S15>(entity =>
        {
            entity.ToTable("S15");

            entity.Property(e => e.ActiveCoActivitySize).HasMaxLength(500);
            entity.Property(e => e.AddueDate)
                .HasMaxLength(500)
                .HasColumnName("ADdueDate");
            entity.Property(e => e.AdsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("ADsubmitDate");
            entity.Property(e => e.Co)
                .HasMaxLength(500)
                .HasColumnName("Co#");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyStatus).HasMaxLength(500);
            entity.Property(e => e.DateReturned).HasMaxLength(500);
            entity.Property(e => e.DateSendtoClient).HasMaxLength(500);
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.SecFileNo).HasMaxLength(500);
            entity.Property(e => e.SsmextensionDateforAcc)
                .HasMaxLength(500)
                .HasColumnName("SSMextensionDateforAcc");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<S16>(entity =>
        {
            entity.ToTable("S16");

            entity.Property(e => e.AppealDate).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.CompanyNo).HasMaxLength(500);
            entity.Property(e => e.CompleteDate).HasMaxLength(500);
            entity.Property(e => e.CompletedDate).HasMaxLength(500);
            entity.Property(e => e.DatePassToTaxDept).HasMaxLength(500);
            entity.Property(e => e.DoneBy).HasMaxLength(500);
            entity.Property(e => e.FormCsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("FormCSubmitDate");
            entity.Property(e => e.IncorpDate).HasMaxLength(500);
            entity.Property(e => e.JobCompleted).HasMaxLength(500);
            entity.Property(e => e.PaymentDate).HasMaxLength(500);
            entity.Property(e => e.PenaltiesRm)
                .HasMaxLength(500)
                .HasColumnName("PenaltiesRM");
            entity.Property(e => e.Ref).HasMaxLength(500);
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.RevisedPenalties).HasMaxLength(500);
            entity.Property(e => e.SOffDocsendtoClient)
                .HasMaxLength(500)
                .HasColumnName("S_OffDocsendtoClient");
            entity.Property(e => e.SsmstrikeoffDate)
                .HasMaxLength(500)
                .HasColumnName("SSMstrikeoffDate");
            entity.Property(e => e.SsmsubmitDate)
                .HasMaxLength(500)
                .HasColumnName("SSMsubmitDate");
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasIndex(e => new { e.JobGroup, e.JobKey }, "IX_ScheduledJobs_JobGroup_JobKey").IsUnique();

            entity.HasIndex(e => e.JobKey, "IX_ScheduledJobs_JobKey");

            entity.HasIndex(e => e.UserId, "IX_ScheduledJobs_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.ScheduledJobs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<SdnBhdClient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.SdnBhdClient).HasForeignKey<SdnBhdClient>(d => d.Id);
        });

        modelBuilder.Entity<SecDeptTaskTemplate>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_SecDeptTaskTemplates_ClientId");

            entity.Property(e => e.AdreturnByClientDate).HasColumnName("ADReturnByClientDate");
            entity.Property(e => e.AdsendToClientDate).HasColumnName("ADSendToClientDate");
            entity.Property(e => e.AdsubmitDate).HasColumnName("ADSubmitDate");
            entity.Property(e => e.ArdueDate).HasColumnName("ARDueDate");
            entity.Property(e => e.ArreturnByClientDate).HasColumnName("ARReturnByClientDate");
            entity.Property(e => e.ArsendToClientDate).HasColumnName("ARSendToClientDate");
            entity.Property(e => e.ArsubmitDate).HasColumnName("ARSubmitDate");

            entity.HasOne(d => d.Client).WithMany(p => p.SecDeptTaskTemplates).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<SecStrikeOffTemplate>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_SecStrikeOffTemplates_ClientId");

            entity.HasIndex(e => e.DoneByUserId, "IX_SecStrikeOffTemplates_DoneByUserId");

            entity.Property(e => e.PenaltiesAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RevisedPenaltiesAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SsmdocSentDate).HasColumnName("SSMDocSentDate");
            entity.Property(e => e.SsmpenaltiesAppealDate).HasColumnName("SSMPenaltiesAppealDate");
            entity.Property(e => e.SsmpenaltiesPaymentDate).HasColumnName("SSMPenaltiesPaymentDate");
            entity.Property(e => e.SsmsubmissionDate).HasColumnName("SSMSubmissionDate");

            entity.HasOne(d => d.Client).WithMany(p => p.SecStrikeOffTemplates).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.DoneByUser).WithMany(p => p.SecStrikeOffTemplates).HasForeignKey(d => d.DoneByUserId);
        });

        modelBuilder.Entity<SystemAuditLog>(entity =>
        {
            entity.HasIndex(e => e.PerformedByUserId, "IX_SystemAuditLogs_PerformedByUserId");

            entity.HasOne(d => d.PerformedByUser).WithMany(p => p.SystemAuditLogs)
                .HasForeignKey(d => d.PerformedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<SystemNotificationLog>(entity =>
        {
            entity.HasIndex(e => new { e.CreatedAt, e.Channel }, "IX_SystemNotificationLogs_CreatedAt_Channel");

            entity.Property(e => e.Recipient).HasMaxLength(255);
            entity.Property(e => e.ResultMessage).HasMaxLength(255);
        });

        modelBuilder.Entity<TaxStrikeOffTemplate>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_TaxStrikeOffTemplates_ClientId");

            entity.Property(e => e.FormCsubmitDate).HasColumnName("FormCSubmitDate");
            entity.Property(e => e.FormEsubmitDate).HasColumnName("FormESubmitDate");
            entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IrbpenaltiesAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IRBPenaltiesAmount");

            entity.HasOne(d => d.Client).WithMany(p => p.TaxStrikeOffTemplates).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<Tx1>(entity =>
        {
            entity.ToTable("TX1");

            entity.Property(e => e.CaTaxCompu).HasMaxLength(500);
            entity.Property(e => e.DatemgmtAccAvailbale).HasMaxLength(500);
            entity.Property(e => e.Despatch).HasMaxLength(500);
            entity.Property(e => e.DraftFormC).HasMaxLength(500);
            entity.Property(e => e.DraftToClientReceived).HasMaxLength(500);
            entity.Property(e => e.DraftToClientSent).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.EstQuartertodo).HasMaxLength(500);
            entity.Property(e => e.Fees).HasMaxLength(500);
            entity.Property(e => e.FormC).HasMaxLength(500);
            entity.Property(e => e.FormCsubmitedDate)
                .HasMaxLength(500)
                .HasColumnName("FormCSubmitedDate");
            entity.Property(e => e.InvDate).HasMaxLength(500);
            entity.Property(e => e.NoOfDays).HasMaxLength(500);
            entity.Property(e => e.PnLanalysis)
                .HasMaxLength(500)
                .HasColumnName("PnLAnalysis");
            entity.Property(e => e.Printing).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.TaxCompuCa)
                .HasMaxLength(500)
                .HasColumnName("TaxCompuCA");
            entity.Property(e => e.TaxDueDate).HasMaxLength(500);
            entity.Property(e => e.TaxPayableRm).HasMaxLength(500);
            entity.Property(e => e.TaxPaymentDate).HasMaxLength(500);
        });

        modelBuilder.Entity<Tx1b>(entity =>
        {
            entity.ToTable("TX1B");

            entity.Property(e => e.AccWkDone).HasMaxLength(500);
            entity.Property(e => e.Amount).HasMaxLength(500);
            entity.Property(e => e.AppealDate).HasMaxLength(500);
            entity.Property(e => e.ClientSentCopy).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.FormCsubmitDate).HasMaxLength(500);
            entity.Property(e => e.FormEsubmitDare).HasMaxLength(500);
            entity.Property(e => e.InvoiceDate).HasMaxLength(500);
            entity.Property(e => e.Irbpenalties)
                .HasMaxLength(500)
                .HasColumnName("IRBPenalties");
            entity.Property(e => e.NoteRemark).HasMaxLength(500);
            entity.Property(e => e.PaymentDate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<Tx2>(entity =>
        {
            entity.ToTable("TX2");

            entity.Property(e => e.Activity).HasMaxLength(500);
            entity.Property(e => e.Aexot)
                .HasMaxLength(500)
                .HasColumnName("AEXOT");
            entity.Property(e => e.Btm)
                .HasMaxLength(500)
                .HasColumnName("BTM");
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateMgmtAccAvailable).HasMaxLength(500);
            entity.Property(e => e.DocPassFrAuditDept).HasMaxLength(500);
            entity.Property(e => e.EstMthTodo).HasMaxLength(500);
            entity.Property(e => e.NetProfit).HasMaxLength(500);
            entity.Property(e => e.Rakc)
                .HasMaxLength(500)
                .HasColumnName("RAKC");
            entity.Property(e => e.Revenue).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.TaxDueDate).HasMaxLength(500);
            entity.Property(e => e.TotalPercent).HasMaxLength(500);
            entity.Property(e => e.TransferToWiptx3)
                .HasMaxLength(500)
                .HasColumnName("TransferToWIPTX3");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<Tx3>(entity =>
        {
            entity.ToTable("TX3");

            entity.Property(e => e.Active).HasMaxLength(500);
            entity.Property(e => e.Aexot)
                .HasMaxLength(500)
                .HasColumnName("AEXOT");
            entity.Property(e => e.Btm)
                .HasMaxLength(500)
                .HasColumnName("BTM");
            entity.Property(e => e.CaTaxCompu).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Completed).HasMaxLength(500);
            entity.Property(e => e.Despatch).HasMaxLength(500);
            entity.Property(e => e.DraftForm).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasMaxLength(500);
            entity.Property(e => e.Fees).HasMaxLength(500);
            entity.Property(e => e.FormC).HasMaxLength(500);
            entity.Property(e => e.FormCsubmited)
                .HasMaxLength(500)
                .HasColumnName("FormCSubmited");
            entity.Property(e => e.InvDate).HasMaxLength(500);
            entity.Property(e => e.NoOfDays).HasMaxLength(500);
            entity.Property(e => e.PnLanalysis)
                .HasMaxLength(500)
                .HasColumnName("PnLAnalysis");
            entity.Property(e => e.Printing).HasMaxLength(500);
            entity.Property(e => e.Rakc)
                .HasMaxLength(500)
                .HasColumnName("RAKC");
            entity.Property(e => e.Received).HasMaxLength(500);
            entity.Property(e => e.Sent).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasMaxLength(500);
            entity.Property(e => e.TaxCompCa)
                .HasMaxLength(500)
                .HasColumnName("TaxCompCA");
            entity.Property(e => e.TaxDueDate).HasMaxLength(500);
            entity.Property(e => e.TaxPayable).HasMaxLength(500);
            entity.Property(e => e.TaxPaymentDate).HasMaxLength(500);
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<Tx4>(entity =>
        {
            entity.ToTable("TX4");

            entity.Property(e => e.AccWkDone).HasMaxLength(500);
            entity.Property(e => e.AmountRm)
                .HasMaxLength(500)
                .HasColumnName("AmountRM");
            entity.Property(e => e.AppealDate).HasMaxLength(500);
            entity.Property(e => e.ClientCopySent).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.DateReceiveFrSecDept).HasMaxLength(500);
            entity.Property(e => e.DateSoff)
                .HasMaxLength(500)
                .HasColumnName("DateSOff");
            entity.Property(e => e.FormCsubmitDate).HasMaxLength(500);
            entity.Property(e => e.FormEsubmitDate).HasMaxLength(500);
            entity.Property(e => e.InvoiceDate).HasMaxLength(500);
            entity.Property(e => e.Irbpenalties)
                .HasMaxLength(500)
                .HasColumnName("IRBpenalties");
            entity.Property(e => e.JobCompletedDate).HasMaxLength(500);
            entity.Property(e => e.NoteRemark).HasMaxLength(500);
            entity.Property(e => e.PaymentDate).HasMaxLength(500);
            entity.Property(e => e.SsmsubmissionDate)
                .HasMaxLength(500)
                .HasColumnName("SSMsubmissionDate");
            entity.Property(e => e.YearEnd).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.StaffId, "IX_Users_StaffId").IsUnique();

            entity.Property(e => e.StaffId)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasComputedColumnSql("('StaffId-'+right('000000'+CONVERT([varchar],[Id]),(6)))", true);
            entity.Property(e => e.Title).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
