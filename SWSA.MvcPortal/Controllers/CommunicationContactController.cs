using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers;

[Route("companies/cm-contact")]
public class CommunicationContactController(
    ICommunicationContactService service
    ) : BaseController
{
    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> CreateStaffContact(CreateCompanyCommunicationContactRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> EditStaffContact(EditCompanyCommunicationContactRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{contactId}/delete")]
    public async Task<IActionResult> DeleteStaffContact([FromRoute] int contactId)
    {
        var result = await service.Delete(contactId);
        return Ok(result);
    }
    #endregion
}
