using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Views.Shared.Components.UpsertWorkAllocation;

public class UpsertWorkAllocationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ClientType type)
    {
        return View(type);
    }
}
