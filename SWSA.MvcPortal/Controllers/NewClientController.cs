using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers;
[Route("clients/create")]

public class NewClientController(
    IMsicCodeService _msicCodeService,
    IClientService _clientService
    ) : BaseController
{

    [Route("sdn-bhd")]
    public async Task<IActionResult> CreateSdnBhd()
    {
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("llp")]
    public async Task<IActionResult> CreateLLPAsync()
    {
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("enterprise")]
    public async Task<IActionResult> CreateEnterpriseAsync()
    {
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("individual")]
    public IActionResult CreateIndividual()
    {
        return View();
    }


    [InternalAjaxOnly]
    [HttpPost("company")]
    public async Task<IActionResult> CreateCompanyAsync(CreateCompanyRequest req)
    {
        var result = await _clientService.CreateCompanyAsync(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("individual")]
    public async Task<IActionResult> CreateIndividualAsync(CreateIndividualRequest req)
    {
        var result = await _clientService.CreateIndividualAsync(req);
        return Ok(result);
    }
}
