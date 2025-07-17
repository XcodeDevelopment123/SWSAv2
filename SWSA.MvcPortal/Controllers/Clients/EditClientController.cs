using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Entities.Clients;
using SWSA.MvcPortal.Models.Clients;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.Clients;

[Route("clients/edit")]
public class EditClientController(
    IMsicCodeService _msicCodeService,
    IClientService _clientService
    ) : BaseController
{

    [Route("sdnbhd/{id:int}")]
    public async Task<IActionResult> EditSdnBhd([FromRoute] int id)
    {
        ViewData["client-type"] = ClientType.SdnBhd;
        var client = (SdnBhdClient)await _clientService.GetEditClientByIdAsync(id);
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyClientEditPageVM<SdnBhdClient>(codes, client);
        return View(vm);
    }

    [Route("llp/{id:int}")]
    public async Task<IActionResult> EditLLP([FromRoute] int id)
    {
        ViewData["client-type"] = ClientType.LLP;
        var client = (LLPClient)await _clientService.GetEditClientByIdAsync(id);
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyClientEditPageVM<LLPClient>(codes, client);
        return View(vm);
    }

    [Route("enterprise/{id:int}")]
    public async Task<IActionResult> EditEnterprise([FromRoute] int id)
    {
        ViewData["client-type"] = ClientType.Enterprise;
        var client = (EnterpriseClient)await _clientService.GetEditClientByIdAsync(id);
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyClientEditPageVM<EnterpriseClient>(codes, client);
        return View(vm);
    }

    [Route("individual/{id:int}")]
    public async Task<IActionResult> EditIndividual([FromRoute] int id)
    {
        ViewData["client-type"] = ClientType.Individual;
        var client = (IndividualClient)await _clientService.GetEditClientByIdAsync(id);
        var vm = new IndividualClientEditPageVM(client);
        return View(vm);
    }

    [InternalAjaxOnly]
    [HttpPost("company")]
    public async Task<IActionResult> UpdateCompanyAsync(UpdateCompanyRequest req)
    {
        var result = await _clientService.UpdateCompanyAsync(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("individual")]
    public async Task<IActionResult> UpdateIndividualAsync(UpdateIndividualRequest req)
    {
        var result = await _clientService.UpdateIndividualAsync(req);
        return Ok(result);
    }

}
