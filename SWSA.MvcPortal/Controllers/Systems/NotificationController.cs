
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces.Systems;

namespace SWSA.MvcPortal.Controllers.Systems;

[Route("notifications")]
public class NotificationController(
    ISystemNotificationLogService service
    ) : BaseController
{
    #region Page/View
    [Route("logs")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetLogs();
        return View(data);
    }
    #endregion
}
