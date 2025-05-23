using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyController(
    ICompanyService service,
    IMsicCodeService msicCodeService,
    IUserService userService
    ) : BaseController
{
    #region Page/View
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetCompanyListVMAsync();
        return View(data);
    }

    [Route("landing-page")]
    public IActionResult LandingPage()
    {
        return View();
    }

    [Route("{companyId}/overview")]
    public async Task<IActionResult> Overview([FromRoute] int companyId)
    {
        var data = await service.GetCompanyByIdAsync(companyId);
        return View(data);
    }

    [Route("create")]
    public async Task<IActionResult> Create()
    {
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        var users = await userService.GetUserSelectionAsync();
        CompanyCreatePageVM vm = new(msicCodes, users);
        return View(vm);
    }

    [Route("{companyId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int companyId)
    {
        var cp = await service.GetCompanyByIdAsync(companyId);
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        var users = await userService.GetUserSelectionAsync();

        CompanyEditPageVM vm = new(cp, msicCodes, users);
        return View(vm);
    }

    [Route("{companyId}/departments")]
    public async Task<IActionResult> CompanyDepartment([FromRoute] int companyId)
    {
        var cp = await service.GetCompanyByIdAsync(companyId);
        return View(cp);
    }
    #endregion

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompanyDetailById([FromRoute] int companyId)
    {
        var data = await service.GetCompanyByIdAsync(companyId);
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpGet("{companyId}/secretary")]
    public async Task<IActionResult> GetCompanyDetailForSecretaryById([FromRoute] int companyId)
    {
        var data = await service.GetCompanyForSecretaryVMByIdAsync(companyId);
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpGet("selections")]
    public async Task<IActionResult> GetCompanySelectionslById()
    {
        var data = await service.GetCompanySelectionAsync();
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCompanyRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> Edit(EditCompanyRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{companyId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int companyId)
    {
        var result = await service.Delete(companyId);
        return Ok(result);
    }
    #endregion
}
