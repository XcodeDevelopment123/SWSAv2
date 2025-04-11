using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.ViewComponents;

public class SelectCompanyViewComponent(
    ICompanyService companyService
    ) : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var companies =await companyService.GetCompanySelectionAsync();
        return View(companies);
    }
}
