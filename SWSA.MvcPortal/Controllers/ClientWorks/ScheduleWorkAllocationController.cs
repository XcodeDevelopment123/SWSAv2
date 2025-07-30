using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.ClientWorks;

namespace SWSA.MvcPortal.Controllers.ClientWorks;

[Route("clients/work-alloc")]
public class ScheduleWorkAllocationController(

    ) : Controller
{
    #region API/Ajax

    [InternalAjaxOnly]
    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleWorkAllocation([FromBody] ScheduleWorkRequest req)
    {

        return Ok();
    }

    #endregion
}
