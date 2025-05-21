using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyService companyService
    ) : BaseController
{
    #region Page/View
    [Route("companies")]
    public async Task<IActionResult> CompanyList()
    {
        var data = await companyService.GetCompanySelectionAsync();
        return View(data);
    }
    #endregion
}
