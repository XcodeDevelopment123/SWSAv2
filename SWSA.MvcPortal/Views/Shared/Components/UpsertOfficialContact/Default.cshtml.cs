using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Views.Shared.Components.UpsertOfficialContact;

public class UpsertOfficialContactViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ClientType type)
    {
        return View(type);
    }
}
