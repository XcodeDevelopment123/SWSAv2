using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.Contacts;

[Route("clients/official-contact")]
public class OfficialContactController(
        IOfficialContactService service
    ) : BaseController
{
    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await service.GetByIdAsync(id);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("")]
    public async Task<IActionResult> UpsertContact(UpsertOfficialContactRequest req)
    {
        var result = await service.UpsertContact(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOfficeContact([FromRoute] int id)
    {
        var result = await service.Delete(id);
        return Ok(result);
    }
    #endregion
}
