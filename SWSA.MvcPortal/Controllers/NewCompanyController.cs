using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

namespace SWSA.MvcPortal.Controllers;
[Route("companies/new")]

public class NewCompanyController(
    ICompanyService service,
    IMsicCodeService msicCodeService
    ) : BaseController
{

    [Route("")]
    public async Task<IActionResult> Create(CompanyType type)
    {
        ViewData["company-type"] = type;
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        CompanyCreatePageVM vm = new(msicCodes, type);
        return View(vm);
    }
}
