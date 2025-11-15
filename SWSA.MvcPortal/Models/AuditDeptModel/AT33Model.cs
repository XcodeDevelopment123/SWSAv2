using System;

namespace SWSA.MvcPortal.Models.AuditDeptModel
{
    public class AT33Model
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? YEtodo { get; set; }
        public string? PIC { get; set; }
        public string? Active { get; set; }
        public string? AEX { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateReviewed { get; set; }
        public DateTime? DateSentToKK { get; set; }
        public DateTime? DateReceivedAFS { get; set; }
        public DateTime? DateofAFS { get; set; }
        public DateTime? DateofDirectorsRept { get; set; }
        public DateTime? MBRSgenerated { get; set; }
        public string? Remark { get; set; }
    }
}