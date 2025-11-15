using System;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AEX51Model
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }

        [Display(Name = "YE to do")]
        public string YEtodo { get; set; }

        [Display(Name = "Quarter to do")]
        public string Quartertodo { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "First 18 mth Due")]
        public DateTime? First18mthDue { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Doc Inwards Date")]
        public DateTime? DocInwardsDate { get; set; }

        [Display(Name = "Revenue")]
        public decimal? Revenue { get; set; }

        [Display(Name = "Profit")]
        public decimal? Profit { get; set; }

        [Display(Name = "Audit Fee")]
        public decimal? AuditFee { get; set; }

        [Display(Name = "Date Billed")]
        public DateTime? DateBilled { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Done Percent")]
        public decimal? DonePercent { get; set; }

        [Display(Name = "Result Over Under")]
        public string ResultOverUnder { get; set; }

        [Display(Name = "Completed")]
        public string Completed { get; set; }

        [Display(Name = "Date Sent")]
        public DateTime? DateSent { get; set; }

        [Display(Name = "Date Sent to KK")]
        public DateTime? DateSenttoKK { get; set; }

        [Display(Name = "Review Result")]
        public string ReviewResult { get; set; }

        [Display(Name = "Date Received from KK")]
        public DateTime? DateReceivedfrKK { get; set; }

        [Display(Name = "Who meet Client Date")]
        public string WhomeetClientDate { get; set; }

        [Display(Name = "Date Sent Client")]
        public DateTime? DateSentClient { get; set; }

        [Display(Name = "Date Received Back")]
        public DateTime? DateReceivedBack { get; set; }

        [Display(Name = "Tax Due Date")]
        public DateTime? TaxDueDate { get; set; }

        [Display(Name = "Pass to Tax Dept")]
        public DateTime? PasstoTaxDept { get; set; }

        [Display(Name = "SSM due Date")]
        public DateTime? SSMdueDate { get; set; }

        [Display(Name = "Date Pass To Sec Dept")]
        public DateTime? DatePassToSecDept { get; set; }

        [Display(Name = "Binded")]
        public string Binded { get; set; }

        [Display(Name = "Despatch Date Client")]
        public DateTime? DespatachDateClient { get; set; }
    }
}