using System;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AEX52Model
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }

        [Display(Name = "Year to do")]
        public string Yeartodo { get; set; }

        [Display(Name = "Quarter to do")]
        public string Quartertodo { get; set; }

        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Audit Fee")]
        public string? AuditFee { get; set; }

        [Display(Name = "Date Billed")]
        public string? DateBilled { get; set; }

        [Display(Name = "Start Date")]
        public string? StartDate { get; set; }

        [Display(Name = "End Date")]
        public string? EndDate { get; set; }

        [Display(Name = "No of Days")]
        public string? NoofDays { get; set; }

        [Display(Name = "Result Over Under")]
        public string ResultOverUnder { get; set; }

        [Display(Name = "Acc Setup")]
        public string? AccSetup { get; set; }

        [Display(Name = "Acc Summary")]
        public string? AccSummary { get; set; }

        [Display(Name = "Audit Planning")]
        public string? AuditPlanning { get; set; }

        [Display(Name = "Audit Execution")]
        public string? AuditExecution { get; set; }

        [Display(Name = "Audit Completion")]
        public string? AuditCompletion { get; set; }

        [Display(Name = "Total Percent")]
        public string? TotalPercent { get; set; }

        [Display(Name = "Review Date Sent")]
        public string? ReviewDateSent { get; set; }

        [Display(Name = "Review End Date")]
        public string? ReviewEndDate { get; set; }

        [Display(Name = "Review Result Over Under")]
        public string ReviewResultOverUnder { get; set; }

        [Display(Name = "KK Date Sent")]
        public string? KKDateSent { get; set; }

        [Display(Name = "KK End Date")]
        public string? KKEndDate { get; set; }

        [Display(Name = "KK Result Over Under")]
        public string KKResultOverUnder { get; set; }

        [Display(Name = "Final")]
        public string Final { get; set; }
    }
}