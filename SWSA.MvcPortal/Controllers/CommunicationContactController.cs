using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Contacts;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers;

[Route("clients/comm-contact")]
public class CommunicationContactController(
    ICommunicationContactService service
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
    public async Task<IActionResult> UpsertContact(UpsertCommunicationContactRequest req)
    {
        var result = await service.UpsertContact(req);
        return Ok(result);
    }


    [InternalAjaxOnly]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] int id)
    {
        var result = await service.Delete(id);
        return Ok(result);
    }
    #endregion
}
