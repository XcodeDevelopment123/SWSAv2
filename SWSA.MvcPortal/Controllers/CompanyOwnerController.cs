using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

namespace SWSA.MvcPortal.Controllers;

[Route("companies/owner")]
public class CompanyOwnerController(
    ICompanyOwnerService service
    ) : BaseController
{
    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOwner(CreateCompanyOwnerRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> EditOwner(EditCompanyOwnerRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{ownerId}/delete")]
    public async Task<IActionResult> DeleteOwner([FromRoute] int ownerId)
    {
        var result = await service.Delete(ownerId);
        return Ok(result);
    }
    #endregion
}
