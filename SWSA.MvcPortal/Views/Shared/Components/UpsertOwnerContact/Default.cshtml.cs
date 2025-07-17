using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Views.Shared.Components.UpsertOwnerContact;

public class UpsertOwnerContactViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ClientType type)
    {
        return View(type);
    }
}
