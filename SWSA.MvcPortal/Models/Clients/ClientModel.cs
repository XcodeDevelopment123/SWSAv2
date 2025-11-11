namespace SWSA.MvcPortal.Models.Clients
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Referral { get; set; }
        public int YearEndMonth { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public int ClientType { get; set; }
        public bool IsActive { get; set; }
    }

    public class A31AModel
    {
        public int Id { get; set; }
        public string Client { get; set; } // 直接存储客户端名称
        public string YearEnded { get; set; }
        public string DateReceived { get; set; }
        public int? NoOfBagBox { get; set; }
        public string ByWhom { get; set; }
        public string UploadLetter { get; set; }
        public string Remark { get; set; }
        public string DateSendToAD { get; set; }
        public string Date { get; set; }
        public int? NoOfBoxBag { get; set; }
        public string ByWhoam2 { get; set; }
        public string UploadLetter2 { get; set; }
        public string Remark2 { get; set; }
    }

    public class A31BModel
    {
        public int Id { get; set; }
        public string Clients { get; set; }
        public string YearEnded { get; set; }
        public string CoStatus { get; set; }
        public string DateDocFr { get; set; }
        public string DateReceived { get; set; }
        public int? NoOfBoxBag { get; set; }
        public string ByWhom { get; set; }
        public string UploadLetter { get; set; }
        public string Remark { get; set; }
        public string Date { get; set; }
        public int? NoOfbox { get; set; }
        public string ByWhom2 { get; set; }
        public string UploadLetter2 { get; set; }
        public string Remark2 { get; set; }
    }
}