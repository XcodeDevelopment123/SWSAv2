namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AT21Model
    {
        public int Id { get; set; }
        public string Grouping { get; set; }
        public string CompanyName { get; set; }
        public string QuartertoAudit { get; set; }
        public string Activity { get; set; }
        public DateTime? YearEnd { get; set; }
        public string YearTodo { get; set; }  // 添加这个缺失的属性
        public string CompanyStatus { get; set; }
        public string AuditStatus { get; set; }
        public string MovetoAEX { get; set; }
        public string MovetoBacklog { get; set; }
        public DateTime? First18mthdue { get; set; }
        public DateTime? AFSdueDate { get; set; }
        public string CoSec { get; set; }
        public string AuditStaff { get; set; }
        public string DateDocIn { get; set; }
        public string EstRev { get; set; }
        public string AcctngWk { get; set; }
        public string JobCompleted { get; set; }
        public string Remark { get; set; }
    }
}