namespace SWSA.MvcPortal.Dtos.Requests.Clients
{
    // 建议放在 Dtos 文件夹中
    public class ClientListDto
    {
        public int Id { get; set; }                 // Client Id
        public string Grouping { get; set; } = "";  // Group AB
        public string Referral { get; set; } = "";
        public string FileNo { get; set; } = "";    // fileNo
        public string CompanyName { get; set; } = "";
        public string CompanyNo { get; set; } = ""; // registrationNumber
        public DateTime? IncorporationDate { get; set; }
        public string YearEndMonth { get; set; } = ""; // e.g. "December"
        public bool IsActive { get; set; }
        public string ClientType { get; set; } = "";

        public string ENumber { get; set; } = "";               // Employer Number (Form E 关键字段)
        public string TaxIdentificationNumber { get; set; } = ""; // TIN
    }
}
