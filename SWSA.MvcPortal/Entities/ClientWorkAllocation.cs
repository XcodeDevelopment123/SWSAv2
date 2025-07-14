using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class ClientWorkAllocation
{

    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(BaseClient))]
    public int ClientId { get; set; }
    public ServiceScope ServiceScope { get; set; }
    //For display to UI
    public string? Label { get; set; }

    //If individual client cannot own this property
    public CompanyActivityLevel CompanyActivityLevel { get; set; }

    //Only for SDN BHD
    public CompanyStatus? CompanyStatus { get; set; }    // For audit dept
    public AuditStatus? AuditStatus { get; set; }    // For audit dept

    public BaseClient Client { get; set; }
}
