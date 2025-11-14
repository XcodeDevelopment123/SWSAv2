namespace SWSA.MvcPortal.Models.AccDeptModel
{
    public class BP34Model
    {
        public int Id { get; set; }
        public string? Item { get; set; }
        public string? Grouping { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? DraftTaxCompleted { get; set; }
        public DateTime? ReviewTax { get; set; }
        public DateTime? FinalTax { get; set; }
        public DateTime? TaxComFinalSignbyClient { get; set; }
        public string? AmountTaxPay { get; set; }
        public DateTime? EFileDraft { get; set; }
        public DateTime? EFileFinal { get; set; }
        public string? TaxRefferanceNo { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? TypeofForm { get; set; }
        public string? SPC { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? DocDespatchDate { get; set; }
    }
}