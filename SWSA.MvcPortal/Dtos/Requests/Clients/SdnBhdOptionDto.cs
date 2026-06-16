namespace SWSA.MvcPortal.Dtos.Requests.Clients
{
    public class SdnBhdOptionDto
    {
        public int Id { get; set; }                 // ClientId
        public string Name { get; set; } = "";      // 公司名
        public DateTime? IncorporationDate { get; set; }  // 成立日期
    }

    public class CompanyOptionDto
    {
        public int Id { get; set; }
        public string Grouping { get; set; } = "";
        public string Referral { get; set; } = "";
        public string FileNo { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string CompanyNo { get; set; } = "";
        public DateTime? IncorporationDate { get; set; }
        public string YearEndMonth { get; set; } = "";
        public string TaxIdentificationNumber { get; set; } = "";
        public string EmployerNumber { get; set; } = "";
        public string ActivitySize { get; set; } = "";
        public string CompanyStatus { get; set; } = "";
        public string CreditRating { get; set; } = "";
        public string AuditExemption { get; set; } = "";
    }

}
