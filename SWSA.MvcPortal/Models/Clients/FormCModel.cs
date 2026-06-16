namespace SWSA.MvcPortal.Models.Clients
{
    public class FormCModel
    {
        public int Id { get; set; }

        // TX Tax Scheduling
        public string? YearEnd { get; set; }
        public string? YearToDo { get; set; }
        public string? TaxDueDate { get; set; }
        public string? EstQuarterTodo { get; set; }
        public string? DateMgmtAccAvailable { get; set; }

        // Tax WIP
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? NoOfDays { get; set; }

        // Tax work process(Date Done)
        public string? PnLAnalysis { get; set; }
        public string? CAnTaxCompu { get; set; }
        public string? DraftFormC { get; set; }

        // Tax payable
        public string? TaxPayableRM { get; set; }
        public string? PenaltiesRM { get; set; }

        // Review
        public string? TaxCompCA { get; set; }
        public string? FormC { get; set; }

        // Date to Client
        public string? Sent { get; set; }
        public string? Received { get; set; }

        // Other dates
        public string? TaxPaymentDate { get; set; }
        public string? FormCsubmitedDate { get; set; }

        // Billing
        public string? InvDate { get; set; }
        public string? Fees { get; set; }

        // MIT+TRS Submitted
        public string? MITSubmitted { get; set; }
        public string? TRSSubmitted { get; set; }

        // Job status
        public string? JobCompleted { get; set; }
    }
}
