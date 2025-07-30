using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.WorkAllocations;

//Just for reference and log, not actually work for year
public class ClientWorkAllocation
{

    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(BaseClient))]
    public int ClientId { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public string? Remarks { get; set; }

    //If individual client cannot own this property
    public CompanyActivityLevel? CompanyActivityLevel { get; set; }

    //Only for SDN BHD
    public AuditCompanyStatus? CompanyStatus { get; set; }    // For audit dept
    public AuditStatus? AuditStatus { get; set; }    // For audit dept

    public BaseClient Client { get; set; }

    [NotMapped]
    public bool IsOther => (int)ServiceScope >= 20;

    public void ChangeServiceScope(ServiceScope service)
    {
        ServiceScope = service;
    }

    public void UpdateInfo(string? remarks, CompanyActivityLevel? companyActivityLevel, AuditCompanyStatus? companyStatus, AuditStatus? auditStatus)
    {
        Remarks = remarks;
        CompanyActivityLevel = companyActivityLevel;
        CompanyStatus = companyStatus;
        AuditStatus = auditStatus;
    }
}
