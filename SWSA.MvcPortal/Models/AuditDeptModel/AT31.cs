namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AT31Model
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Activity { get; set; }
        public string YEtoDo { get; set; }
        public string QuartertoDo { get; set; }
        public string PIC { get; set; }
        public DateTime? MthDue { get; set; }
        public string Status { get; set; }
        public DateTime? DocInwardsDate { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Profit { get; set; }
        public decimal? AuditFee { get; set; }
        public DateTime? DateBilled { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysDone { get; set; }
        public decimal? DonePercent { get; set; }
        public string Completed { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateSentToKK { get; set; }
        public string ReviewResultofDays { get; set; }
        public DateTime? DateReceiveFromKK { get; set; }
        public string WhoMeetClientDate { get; set; }
        public DateTime? DateSenttoClient { get; set; }
        public DateTime? DateReceiveBack { get; set; }
        public DateTime? TaxDueDate { get; set; }
        public DateTime? PasstoDept { get; set; }
        public DateTime? SSMdueDate { get; set; }
        public DateTime? DatePasstoSecDept { get; set; }
        public string Binded { get; set; }
        public DateTime? DespatchDateToClient { get; set; }
    }
}
