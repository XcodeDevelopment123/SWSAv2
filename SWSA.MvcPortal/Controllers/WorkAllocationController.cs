using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers;

[Route("clients/work-alloc")]
public class WorkAllocationController(
    IWorkAllocationService service
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
    public async Task<IActionResult> UpsertWorkAlloc(UpsertWorkAllocationRequest req)
    {
        var result = await service.UpsertWorkAlloc(req);
        return Ok(result);
    }


    [InternalAjaxOnly]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkAlloc([FromRoute] int id)
    {
        var result = await service.Delete(id);
        return Ok(result);
    }
    #endregion
}
