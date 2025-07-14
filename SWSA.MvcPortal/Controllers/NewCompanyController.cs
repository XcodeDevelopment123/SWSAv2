using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;
[Route("companies/new")]

public class NewCompanyController(
    IMsicCodeService msicCodeService
    ) : BaseController
{

    [Route("")]
    public async Task<IActionResult> Create(ClientType type)
    {
        ViewData["company-type"] = type;
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        CompanyCreatePageVM vm = new(msicCodes, type);
        return View(vm);
    }
}
