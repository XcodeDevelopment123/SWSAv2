using System;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AEX42Model
    {
        public int Id { get; set; }

        [Display(Name = "Grouping")]
        public string? Grouping { get; set; }

        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }

        [Display(Name = "Quarter To Do Audit")]
        public string? QuarterToDoAudit { get; set; }

        [Display(Name = "Activity")]
        public string? Activity { get; set; }

        [Display(Name = "Year End")]
        public string? YearEnd { get; set; }

        [Display(Name = "Year To Do")]
        public string? YearToDo { get; set; }

        [Display(Name = "Move To Active Sch")]
        public string? MoveToActiveSch { get; set; }

        [Display(Name = "Date Doc In")]
        public string? DateDocIn { get; set; }

        [Display(Name = "Acctng Wk")]
        public string? AcctngWk { get; set; }

        [Display(Name = "Company Status")]
        public string? CompanyStatus { get; set; }

        [Display(Name = "Audit Exemption")]
        public string? AuditExemption { get; set; }

        [Display(Name = "Co Sec")]
        public string? CoSec { get; set; }

        [Display(Name = "Credit Rating")]
        public string? CreditRating { get; set; }

        [Display(Name = "Signing Firm")]
        public string? SigningFirm { get; set; }

        [Display(Name = "Reason Why Backlog")]
        public string? ReasonWhyBacklog { get; set; }
    }
}