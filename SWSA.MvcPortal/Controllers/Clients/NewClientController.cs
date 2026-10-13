using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Clients;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;

namespace SWSA.MvcPortal.Controllers.Clients;
[Route("clients/create")]

public class NewClientController(
    IMsicCodeService _msicCodeService,
    IClientService _clientService
    ) : BaseController
{

    [Route("sdnbhd")]
    public async Task<IActionResult> CreateSdnBhd()
    {
        ViewData["client-type"] = ClientType.SdnBhd;
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("llp")]
    public async Task<IActionResult> CreateLLP()
    {
        ViewData["client-type"] = ClientType.LLP;
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("enterprise")]
    public async Task<IActionResult> CreateEnterprise()
    {
        ViewData["client-type"] = ClientType.Enterprise;
        var codes = await _msicCodeService.GetMsicCodeAsync();
        var vm = new CompanyCreatePageVM(codes);
        return View(vm);
    }

    [Route("individual")]
    public IActionResult CreateIndividual()
    {
        ViewData["client-type"] = ClientType.Individual;
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
    [HttpPost("sdnbhd/import")]
    public async Task<IActionResult> ImportSdnBhdAsync([FromBody] ImportCompanyRequest req)
    {
        if (req?.Rows == null || req.Rows.Count == 0)
            return BadRequest(new { message = "No rows found in the uploaded file." });

        if (req.Rows.Count > 1000)
            return BadRequest(new { message = "Maximum 1000 rows per import." });

        var result = await _clientService.ImportCompaniesAsync(ClientType.SdnBhd, CompanyType.SdnBhd, req.Rows);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("llp/import")]
    public async Task<IActionResult> ImportLLPAsync([FromBody] ImportCompanyRequest req)
    {
        if (req?.Rows == null || req.Rows.Count == 0)
            return BadRequest(new { message = "No rows found in the uploaded file." });

        if (req.Rows.Count > 1000)
            return BadRequest(new { message = "Maximum 1000 rows per import." });

        var result = await _clientService.ImportCompaniesAsync(ClientType.LLP, CompanyType.LLP, req.Rows);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("enterprise/import")]
    public async Task<IActionResult> ImportEnterpriseAsync([FromBody] ImportCompanyRequest req)
    {
        if (req?.Rows == null || req.Rows.Count == 0)
            return BadRequest(new { message = "No rows found in the uploaded file." });

        if (req.Rows.Count > 1000)
            return BadRequest(new { message = "Maximum 1000 rows per import." });

        var result = await _clientService.ImportCompaniesAsync(ClientType.Enterprise, CompanyType.SoleProprietorships, req.Rows);
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
