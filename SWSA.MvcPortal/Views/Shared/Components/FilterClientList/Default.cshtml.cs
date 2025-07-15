using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Views.Shared.Components.FilterClientList;

public class FilterClientListViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ClientType type)
    {
        return View(type);
    }
}
