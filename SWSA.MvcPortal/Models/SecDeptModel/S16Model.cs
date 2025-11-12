namespace SWSA.MvcPortal.Models.SecDeptModel
{
    public class S16Model
    {
        public int Id { get; set; }
        public string? Ref { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyNo { get; set; }
        public string? IncorpDate { get; set; }
        public string? YearEnd { get; set; }
        public string? StartDate { get; set; }
        public string? CompleteDate { get; set; }
        public string? DoneBy { get; set; }
        public string? CompletedDate { get; set; }
        public string? PenaltiesRM { get; set; }
        public string? RevisedPenalties { get; set; }
        public string? AppealDate { get; set; }
        public string? PaymentDate { get; set; }
        public string? S_OffDocsendtoClient { get; set; }
        public string? SSMsubmitDate { get; set; }
        public string? SSMstrikeoffDate { get; set; }
        public string? Remark { get; set; }
        public string? Action { get; set; }
        public string? DatePassToTaxDept { get; set; }
        public string? FormCSubmitDate { get; set; }
        public string? JobCompleted { get; set; }
    }

    public class S16CompanyModel
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? YearEnd { get; set; }
        public string? SSMsubmitDate { get; set; }
        public string? SSMstrikeoffDate { get; set; }
        public string? DatePassToTaxDept { get; set; }
        public string? FormCSubmitDate { get; set; }
    }
}
