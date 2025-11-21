namespace SWSA.MvcPortal.Entities.Clients
{
    public class SdnBhdOptionDto
    {
        public int Id { get; set; }                 // ClientId
        public string Name { get; set; } = "";      // 公司名
        public DateTime? IncorporationDate { get; set; }  // 成立日期
    }

    public class CompanyOptionDto
    {
        public int Id { get; set; }                 // Client Id
        public string Grouping { get; set; } = "";  // Group AB
        public string Referral { get; set; } = "";
        public string FileNo { get; set; } = "";    // fileNo
        public string CompanyName { get; set; } = "";
        public string CompanyNo { get; set; } = ""; // registrationNumber
        public DateTime? IncorporationDate { get; set; }
        public string YearEndMonth { get; set; } = ""; // e.g. "December"
        public string TaxIdentificationNumber { get; set; } = ""; // e.g. "December"
        public string EmployerNumber { get; set; } = ""; // e.g. "December"
    }

}
