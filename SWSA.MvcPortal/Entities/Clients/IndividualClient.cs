using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;

namespace SWSA.MvcPortal.Entities.Clients;

public class IndividualClient : BaseClient
{

    [SystemAuditLog("IC or Passport Number")]
    public string ICOrPassportNumber { get; set; } = null!;

    [SystemAuditLog("Professions")]
    public string Profession { get; set; }

    public void CreateWorkAlloc()
    {
        var formBeExist = WorkAllocations.Any(a => a.ServiceScope == ServiceScope.FormBE);

        if (formBeExist)
        {
            throw new BusinessLogicException("Individual client only can have one form BE service");
        }

        WorkAllocations.Add(new ClientWorkAllocation()
        {
            ServiceScope = ServiceScope.FormBE,
            Remarks = "Form BE",
        });
    }
}
