using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Templates;

public class AuditTemplate
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int? PersonInChargeId { get; set; }
    public AuditDatabase Database { get; set; } = AuditDatabase.Active;
    public AuditStatus Status { get; set; } = AuditStatus.Pending;
    public DateTime YearEndToDo { get; set; }
    public int QuarterToDo { get; set; }

    //Est current yr result
    public decimal? Revenue { get; set; }
    public decimal? Profit { get; set; }
    //Biling
    public decimal? AuditFee { get; set; }
    public DateTime? DateBilled { get; set; }

    //Audit WIP (days)
    public DateTime AuditStartDate { get; set; }
    public DateTime AuditEndDate { get; set; }
    public int NumberOfDays => (AuditEndDate - AuditStartDate).Days;
    public int TotalFieldWorkDays { get; set; } = 0;
    public AuditVarianceType? AuditWIPResult { get; set; }

    //Audit work WIP (Percent)
    public bool IsAccSetupComplete { get; set; } = false;
    public bool IsAccSummaryComplete { get; set; } = false;
    public bool IsAuditPlanningComplete { get; set; } = false;
    public bool IsAuditExecutionComplete { get; set; } = false;
    public bool IsExecutionAuditComplete { get; set; } = false;

    [NotMapped]
    public string ProgressInPercent
    {
        get
        {
            int totalSteps = 5;
            int completedSteps = 0;
            if (IsAccSetupComplete) completedSteps++;
            if (IsAccSummaryComplete) completedSteps++;
            if (IsAuditPlanningComplete) completedSteps++;
            if (IsAuditExecutionComplete) completedSteps++;
            if (IsExecutionAuditComplete) completedSteps++;
            return $"{completedSteps * 100 / totalSteps}%";
        }
    }

    //Review
    public DateTime? FirstReviewSendDate { get; set; }
    public DateTime? FirstReviewEndDate { get; set; }
    public AuditVarianceType? FirstReviewResult { get; set; }
    public DateTime? SecondReviewSendDate { get; set; }
    public DateTime? SecondReviewEndDate { get; set; }
    public AuditVarianceType? SecondReviewResult { get; set; }

    public int TotalReviewDays
    {
        get
        {
            int totalDays = 0;
            if (FirstReviewSendDate.HasValue && FirstReviewEndDate.HasValue)
            {
                totalDays += (FirstReviewEndDate.Value - FirstReviewSendDate.Value).Days;
            }
            if (SecondReviewSendDate.HasValue && SecondReviewEndDate.HasValue)
            {
                totalDays += (SecondReviewEndDate.Value - SecondReviewSendDate.Value).Days;
            }
            return totalDays;
        }
    }

    //Audit Report from KK
    public DateTime? KualaLumpurOfficeDateSent { get; set; }
    public DateTime? KualaLumpurOfficeAuditReportReceivedDate { get; set; }
    public DateTime? KualaLumpurOfficeReportDate { get; set; }
    public DateTime? KualaLumpurOfficeDirectorsReportDate { get; set; }

    //Director Signing pages

    public DateTime? DirectorDateSent { get; set; }
    public DateTime? DirectorFollowUpDate { get; set; }
    public DateTime? DirectorDateReceived { get; set; }
    public DateTime? DirectorCommOfOathsDate { get; set; }

    //Tax Dept
    public DateTime? TaxDueDate { get; set; }
    public DateTime? DatePassToTaxDept { get; set; }
    //Sec Dept
    public DateTime? SecSSMDueDate { get; set; }
    public DateTime? DatePassToSecDept { get; set; }

    //POST Audit Work
    public DateTime? PostAuditDateBinded { get; set; }
    public DateTime? PostAuditDespatchDateToClient { get; set; }


    [ForeignKey(nameof(PersonInChargeId))]
    public User PersonInCharge { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    public BaseCompany Client { get; set; } = null!;
}

public enum AuditVarianceType
{
    Over, Under,
}

public enum AuditStatus
{
    [Display(Name = "Pending")]
    Pending,
    [Display(Name = "Work In Progress")]
    Audit_WIP,
    [Display(Name = "First Review")]
    Audit_FirstReview,
}

public enum AuditDatabase
{
    Active,
    [Display(Name = "AEX")]
    AEX,
    Backlog
}