using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;

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
    public async Task<IActionResult> List(ClientType? type)
    {
        var data = type.HasValue ? await service.GetCompaniesByTypeAsync(type.Value) : await service.GetCompaniesAsync();
        ViewData["company-type"] = type;
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


    [Route("{companyId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int companyId)
    {
        var msicCodes = await msicCodeService.GetMsicCodeAsync();

        CompanyEditPageVM vm = new( msicCodes);
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
    [HttpGet("{companyId}/simple-info")]
    public async Task<IActionResult> GetCompanySimpleInfoById([FromRoute] int companyId)
    {
        var data = await service.GetCompanySimpleInfoVMByIdAsync(companyId);
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
