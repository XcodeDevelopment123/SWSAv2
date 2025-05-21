using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies/official-contact")]
public class CompanyOfficialContactController(
        ICompanyOfficialContactService service
    ) : BaseController
{
    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOfficeContact(CreateCompanyOfficialContactRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> EditOfficeContact(EditCompanyOfficialContactRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{contactId}/delete")]
    public async Task<IActionResult> DeleteOfficeContact([FromRoute] int contactId)
    {
        var result = await service.Delete(contactId);
        return Ok(result);
    }
    #endregion
}
