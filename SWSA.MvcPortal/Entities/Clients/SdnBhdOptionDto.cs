namespace SWSA.MvcPortal.Entities.Clients
{
    public class SdnBhdOptionDto
    {
        public int Id { get; set; }                 // ClientId
        public string Name { get; set; } = "";      // 公司名
        public DateTime? IncorporationDate { get; set; }  // 成立日期
    }
}
