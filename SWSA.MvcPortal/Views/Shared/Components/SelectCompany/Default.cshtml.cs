using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;

namespace SWSA.MvcPortal.Views.Shared.Components.SelectCompany;

public class SelectCompanyViewComponent(
    ICompanyService companyService
    ) : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var companies = await companyService.GetCompanySelectionAsync();
        return View(companies);
    }
}
