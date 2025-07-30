using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities.Systems;

public class MsicCode
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string CategoryReferences { get; set; }
    public ICollection<CompanyMsicCode> CompanyLinks { get; set; } = new List<CompanyMsicCode>();

}
