using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyService companyService
    ) : BaseController
{
    [Route("companies")]
    public async Task<IActionResult> CompanyList()
    {
        var data = await companyService.GetCompanySelectionAsync();    
        return View(data);
    }

}
