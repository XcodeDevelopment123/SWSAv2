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
        public decimal? AuditFee { get; set; }

        [Display(Name = "Date Billed")]
        public DateTime? DateBilled { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "No of Days")]
        public int? NoofDays { get; set; }

        [Display(Name = "Result Over Under")]
        public string ResultOverUnder { get; set; }

        [Display(Name = "Acc Setup")]
        public decimal? AccSetup { get; set; }

        [Display(Name = "Acc Summary")]
        public decimal? AccSummary { get; set; }

        [Display(Name = "Audit Planning")]
        public decimal? AuditPlanning { get; set; }

        [Display(Name = "Audit Execution")]
        public decimal? AuditExecution { get; set; }

        [Display(Name = "Audit Completion")]
        public decimal? AuditCompletion { get; set; }

        [Display(Name = "Total Percent")]
        public decimal? TotalPercent { get; set; }

        [Display(Name = "Review Date Sent")]
        public DateTime? ReviewDateSent { get; set; }

        [Display(Name = "Review End Date")]
        public DateTime? ReviewEndDate { get; set; }

        [Display(Name = "Review Result Over Under")]
        public string ReviewResultOverUnder { get; set; }

        [Display(Name = "KK Date Sent")]
        public DateTime? KKDateSent { get; set; }

        [Display(Name = "KK End Date")]
        public DateTime? KKEndDate { get; set; }

        [Display(Name = "KK Result Over Under")]
        public string KKResultOverUnder { get; set; }

        [Display(Name = "Final")]
        public string Final { get; set; }
    }
}