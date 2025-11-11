namespace SWSA.MvcPortal.Models.Clients
{
    public partial class A32a
    {
        public int Id { get; set; }
        public string? CaseNo { get; set; }
        public string? DateReceived { get; set; }
        public string? TypeIncoming { get; set; }
        public string? Client { get; set; }
        public string? YearAssessment { get; set; }
        public string? Details { get; set; }
        public string? Date { get; set; }
        public string? BriefDescritions { get; set; }
        public string? Pic { get; set; } // Dapper 会自动映射 PIC 列到 Pic 属性
        public string? Remark { get; set; }
        public string? DoneOn { get; set; }
    }
}
