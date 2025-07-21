using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SWSA.MvcPortal.Entities.Clients;

namespace SWSA.MvcPortal.Entities;

public class CompanyMsicCode
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    [ForeignKey(nameof(MsicCode))]
    public int MsicCodeId { get; set; }
    public BaseCompany Company { get; set; } = null!;
    public MsicCode MsicCode { get; set; } = null!;

    public CompanyMsicCode(int msicCodeId)
    {
        MsicCodeId = msicCodeId;
    }
}
