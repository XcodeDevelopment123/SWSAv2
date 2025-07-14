using SWSA.MvcPortal.Commons.Attributes;

namespace SWSA.MvcPortal.Entities.Clients;

public class IndividualClient : BaseClient
{

    [SystemAuditLog("IC or Passport Number")]
    public string ICOrPassportNumber { get; set; } = null!;

    [SystemAuditLog("Professions")]
    public string Profession { get; set; }
}
