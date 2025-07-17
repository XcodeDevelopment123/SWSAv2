using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.Clients;

public class UpsertWorkAllocationRequest
{
    public int? Id { get; set; } //entity id, if have value is update
    public int ClientId { get; set; }
    public ServiceScope Service { get; set; }
    public string? Remarks { get; set; }

    public CompanyActivityLevel? ActivitySize { get; set; }

    public AuditCompanyStatus? AuditCpStatus { get; set; }   
    public AuditStatus? AuditStatus { get; set; }  
}
