using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("sys-audit-logs")]
public class SystemAuditLogController(
    ISystemAuditLogService service
    ) : BaseController
{
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetLogs();
        return View(data);
    }

    [HttpGet("{logId}")]
    public async Task<IActionResult> GetChangeSumamriesById([FromRoute] int logId)
    {
        var data = await service.GetLogById(logId);
        return Ok(data);
    }
}
