using SWSA.MvcPortal.Commons.Filters;
using SWSA.MvcPortal.Entities.ValueOfObject;

namespace SWSA.MvcPortal.Entities;

//For Actual Submit record & Record Client Sign/Return record
public class AnnualReturnSubmission : BaseSubmission
{
    [SystemAuditLog("Submission Year")]
    public int Year { get; set; } // e.g. 2025

    [SystemAuditLog("Anniversary Date")]
    public DateTime? AnniversaryDate { get; set; } //EST 17 months from  incopr date

    [SystemAuditLog("Targeted AR Date")]
    public DateTime? TargetedARDate { get; set; } // 7 Month from year end

    [SystemAuditLog("Actual Date of Annual Return")]
    public DateTime? DateOfAnnualReturn { get; set; }

    [SystemAuditLog("Date AR Submitted")]
    public DateTime? DateSubmitted { get; set; }

    [SystemAuditLog("Date Sent to Client")]
    public DateTime? DateSentToClient { get; set; }

    [SystemAuditLog("Date Returned by Client")]
    public DateTime? DateReturnedByClient { get; set; }

    public AnnualReturnSubmission() { }

    public AnnualReturnSubmission(Company cp)
    {
        Year = DateTime.Now.Year;
        if (cp.IncorporationDate.HasValue)
            AnniversaryDate = cp.IncorporationDate.Value.AddMonths(17);

        if (cp.YearEndMonth.HasValue)
        {
            int year = DateTime.Now.Year;
            int month = (int)cp.YearEndMonth.Value;
            DateTime yearEnd = new DateTime(year, month, 1);
            TargetedARDate = yearEnd.AddMonths(7);
        }
    }

}