namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AT32Model
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Activity { get; set; }
        public string YeartoDo { get; set; }
        public string Quartertodo { get; set; }
        public string PIC { get; set; }
        public string Status { get; set; }
        public decimal? AuditFee { get; set; }
        public DateTime? DateBilled { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NoOfDays { get; set; }
        public int? TotalFieldwkDays { get; set; }
        public string FinalResultOverUnder { get; set; }
        public decimal? AccSetup { get; set; }
        public decimal? Assummary { get; set; }
        public decimal? AuditPlanning { get; set; }
        public decimal? AuditExecution { get; set; }
        public decimal? AuditCompleion { get; set; }
        public decimal? TotalPercent { get; set; }
        public DateTime? ReviewDateSent { get; set; }
        public DateTime? ReviewEndDate { get; set; }
        public int? ReviewOfDays { get; set; }
        public DateTime? KKdateSent { get; set; }
        public DateTime? KKendDate { get; set; }
        public int? KKofDate { get; set; }
        public int? TotalReviewDays { get; set; }
    }
}
