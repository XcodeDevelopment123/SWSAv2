namespace SWSA.MvcPortal.Models.SecDeptModel
{
    public class S13A
    {
        public int Id { get; set; }
        public string? Grouping { get; set; }
        public string? Referral { get; set; }
        public string? SecFileNo { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyType { get; set; }
        public string? YearEnd { get; set; }
        public string? CompanyStatus { get; set; }
        public string? ActiveCoActivitySize { get; set; }
        public string? YEtoDo { get; set; }
        public string? ACCmthTodo { get; set; }
        public string? AuditMthTodo { get; set; }
        public string? YrMthDueDate { get; set; }
        public string? Circulation { get; set; }
        public string? ARdueDate { get; set; }
        public string? AFSSubmitDate { get; set; }
        public string? ARSubmitDate { get; set; }
        public string? JobCompleted { get; set; }

        // 🔸 新增：只用于传去 S14B，不一定要是 DB column
        public string? CompanyNo { get; set; }
        public string? IncorpDate { get; set; }
    }
}
