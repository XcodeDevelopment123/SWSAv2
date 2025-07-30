
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.Clients;

[Route("clients")]
public class ClientController(
    IClientService _service,
    IWorkAllocationService _workAllocService,
    IClientOptionService _optionService
    ) : BaseController
{
    [Route("sdnbhd")]
    public IActionResult SdnBhdList()
    {
        return View();
    }

    [Route("llp")]
    public IActionResult LLPList()
    {
        return View();
    }

    [Route("enterprise")]
    public IActionResult EnterpriseList()
    {
        return View();
    }

    [Route("individual")]
    public IActionResult IndividualList()
    {
        return View();
    }

    [InternalAjaxOnly]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetClientById([FromRoute] int id)
    {
        var data = await _service.GetClientWithDetailByIdAsync(id);
        return Ok(data);
    }

    [HttpGet("{id:int}/work-allocs")]
    public async Task<IActionResult> GetWorkAllocationsByClientId([FromRoute] int id)
    {
        var data = await _workAllocService.GetWorksByClientId(id);
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpGet("{id:int}/simple-info")]
    public async Task<IActionResult> GetClientSimpleInfo([FromRoute] int id)
    {
        var data = await _service.GetClientByIdAsync<BaseClient>(id);
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromQuery] ClientType type, ClientFilterRequest req)
    {
        var data = await _service.SearchClientsAsync(type, req);
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpPost("options")]
    public async Task<IActionResult> GetOptionValues(ClientOptionRequest req)
    {
        var data = await _optionService.GetOptionValuesAsync(req);
        return Ok(data);
    }
}

