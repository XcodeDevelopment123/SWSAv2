using System;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AEX11Model
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        public string Activity { get; set; }
        public string YEtodo { get; set; }
        public string QuarterTodo { get; set; }
        public string PIC { get; set; }
        public string Status { get; set; }

        public string Revenue { get; set; }
        public string ProfitLoss { get; set; }
        public string AuditFee { get; set; }
        public string DateBilled { get; set; }

        public string StartDate { get; set; }
        public string AsAt { get; set; }
        public string NoOfDays { get; set; }
        public string ResultOverUnder { get; set; }

        public string AccSetup { get; set; }
        public string AccSummary { get; set; }
        public string AuditPlanning { get; set; }
        public string AuditExecution { get; set; }
        public string AuditCompletion { get; set; }
        public string TotalPercent { get; set; }

        public string DateSentKuching { get; set; }
        public string EndDateKuching { get; set; }
        public string ResultOverUnderKuching { get; set; }

        public string DateSentKK { get; set; }
        public string EndDateKK { get; set; }
        public string ResultOverUnderKK { get; set; }
        public string Final { get; set; }

        public string DateSentToKK { get; set; }
        public string DateReceivedAR { get; set; }
        public string DateReport { get; set; }
        public string DateOfDirectorRept { get; set; }

        public string DateSentSigning { get; set; }
        public string FlwUpDate { get; set; }
        public string DateReceived { get; set; }
        public string CommOfOathsDate { get; set; }

        public string TaxDueDate { get; set; }
        public string PassToTax { get; set; }

        public string SSMDueDate { get; set; }
        public string DatePassToSecDept { get; set; }

        public string DateBinded { get; set; }
        public string DespatchDateToClient { get; set; }
    }
}