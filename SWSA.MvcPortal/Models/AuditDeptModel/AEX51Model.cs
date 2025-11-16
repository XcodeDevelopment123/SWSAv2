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
        public string? First18mthDue { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Doc Inwards Date")]
        public string? DocInwardsDate { get; set; }

        [Display(Name = "Revenue")]
        public string? Revenue { get; set; }

        [Display(Name = "Profit")]
        public string? Profit { get; set; }

        [Display(Name = "Audit Fee")]
        public string? AuditFee { get; set; }

        [Display(Name = "Date Billed")]
        public string? DateBilled { get; set; }

        [Display(Name = "Start Date")]
        public string? StartDate { get; set; }

        [Display(Name = "End Date")]
        public string? EndDate { get; set; }

        [Display(Name = "Done Percent")]
        public string? DonePercent { get; set; }

        [Display(Name = "Result Over Under")]
        public string ResultOverUnder { get; set; }

        [Display(Name = "Completed")]
        public string Completed { get; set; }

        [Display(Name = "Date Sent")]
        public string? DateSent { get; set; }

        [Display(Name = "Date Sent to KK")]
        public string? DateSenttoKK { get; set; }

        [Display(Name = "Review Result")]
        public string ReviewResult { get; set; }

        [Display(Name = "Date Received from KK")]
        public string? DateReceivedfrKK { get; set; }

        [Display(Name = "Who meet Client Date")]
        public string WhomeetClientDate { get; set; }

        [Display(Name = "Date Sent Client")]
        public string? DateSentClient { get; set; }

        [Display(Name = "Date Received Back")]
        public string? DateReceivedBack { get; set; }

        [Display(Name = "Tax Due Date")]
        public string? TaxDueDate { get; set; }

        [Display(Name = "Pass to Tax Dept")]
        public string? PasstoTaxDept { get; set; }

        [Display(Name = "SSM due Date")]
        public string? SSMdueDate { get; set; }

        [Display(Name = "Date Pass To Sec Dept")]
        public string? DatePassToSecDept { get; set; }

        [Display(Name = "Binded")]
        public string Binded { get; set; }

        [Display(Name = "Despatch Date Client")]
        public string? DespatachDateClient { get; set; }
    }
}