using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Views.Shared.Components.SelectCompany;

public class ScheduleWorkModalViewComponent(
    ) : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(null);
    }
}
