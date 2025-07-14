using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Views.Shared.Components.SelectCompany;

public class SelectCompanyViewComponent(
    ) : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(null);
    }
}
