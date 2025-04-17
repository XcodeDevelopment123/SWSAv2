
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("notifications")]
public class NotificationController(
    ISystemNotificationLogService service
    ) : BaseController
{

    [Route("logs")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetLogs();
        return View(data);
    }
}
