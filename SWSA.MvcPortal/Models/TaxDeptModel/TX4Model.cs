namespace SWSA.MvcPortal.Models.TaxDeptModel
{
    public class TX4Model
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? YearEnd { get; set; }
        public string? SSMsubmissionDate { get; set; }
        public string? DateSOff { get; set; }
        public string? DateReceiveFrSecDept { get; set; }
        public string? IRBpenalties { get; set; }
        public string? AppealDate { get; set; }
        public string? PaymentDate { get; set; }
        public string? NoteRemark { get; set; }
        public string? AccWkDone { get; set; }
        public string? FormCsubmitDate { get; set; }
        public string? FormEsubmitDate { get; set; }
        public string? InvoiceDate { get; set; }
        public string? AmountRM { get; set; }
        public string? ClientCopySent { get; set; }
        public string? JobCompletedDate { get; set; }


        public string? LinkedSSMsubmitDate { get; set; }
        public string? LinkedSSMstrikeoffDate { get; set; }
        public string? LinkedDatePassToTaxDept { get; set; }
        public string? LinkedFormCSubmitDate { get; set; }

        // ★ 新增：仅用于同步到 S16，不写入 TX4 表
        public string? Referral { get; set; }
        public string? CompanyNo { get; set; }
        public string? IncorporationDate { get; set; }   // 建议用 dd/MM/yyyy 字串即可
    }
}
