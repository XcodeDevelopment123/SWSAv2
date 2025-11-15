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
        public DateTime? DateDocIn { get; set; }

        [Display(Name = "Acctng Wk")]
        public string? AcctngWk { get; set; }

        [Display(Name = "Reason Why Backlog")]
        public string? ReasonWhyBacklog { get; set; }
    }
}