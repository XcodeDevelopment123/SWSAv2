using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Views.Shared.Components.ClientListDetails;

public class ClientListDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ClientType type)
    {
        return View(type);
    }
}
