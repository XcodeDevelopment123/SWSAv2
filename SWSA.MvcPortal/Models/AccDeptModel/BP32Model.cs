using System;

namespace SWSA.MvcPortal.Models.AccDeptModel
{
    public class BP32Model
    {
        public int Id { get; set; }

        // Basic Information
        public string FileNo { get; set; }
        public string CompanyName { get; set; }
        public string YearEnd { get; set; }

        // Company Details
        public string JobServices { get; set; }
        public string CoStatus { get; set; }
        public string ActiveCoActivitySize { get; set; }
        public string YEtodo { get; set; }
        public string MthTodo { get; set; }
        public string DocReceivedDate { get; set; }

        // Scheduling & Timeline
        public string TaxARdueDate { get; set; }
        public string Staff { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TimeTaken { get; set; }
        public string DatePassToAudit { get; set; }
        public string DateTaxSubmited { get; set; }
        public string CompletedBacklog { get; set; }

        // Tax Process Steps
        public string SingleEntry { get; set; }
        public string TaxComputation { get; set; }
        public string SortingFiling { get; set; }
        public string KeyinToExcel { get; set; }
        public string ReviewWorkingAcc { get; set; }
        public string DraftFinancialStatement { get; set; }
        public string DraftTaxCompleted { get; set; }
        public string ReviewTax { get; set; }
        public string FinalTax { get; set; }
        public string TaxComFinalSignByClient { get; set; }

        // E-Filing & Billing
        public string AmountTaxPay { get; set; }
        public string EFileDraft { get; set; }
        public string EFileFinal { get; set; }
        public string EFileviaSPC { get; set; }
        public string InvoiceNo { get; set; }
        public string AmountRM { get; set; }
        public string DocDespatchDate { get; set; }
    }
}