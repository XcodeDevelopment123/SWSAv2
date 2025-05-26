using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

namespace SWSA.MvcPortal.Controllers;

[Route("companies/compliance-date")]
public class CompanyComplianceDateController(
    ICompanyComplianceDateService service
    ) : BaseController
{
    #region API/Ajax

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> EditComplianceDate(EditCompanyComplianceDate req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }
    #endregion
}
