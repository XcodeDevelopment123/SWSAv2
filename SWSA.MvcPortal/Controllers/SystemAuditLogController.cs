using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("sys-audit-logs")]
public class SystemAuditLogController(
    ISystemAuditLogService service
    ) : BaseController
{
    #region Page/View
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetLogs();
        return View(data);
    }
    #endregion

    #region API/Ajax

    [InternalAjaxOnly]
    [HttpGet("{logId}")]
    public async Task<IActionResult> GetChangeSumamriesById([FromRoute] int logId)
    {
        var data = await service.GetLogById(logId);
        return Ok(data);
    }
    #endregion
}
