using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers;

[Route("samples")]
public class SampleController : BaseController
{
    [Route("at1.2")]
    public IActionResult AT12AuditReminderSchedule()
    {
        return View();
    }

    [Route("at2.1-master-log")]
    public IActionResult AT21AuditMasterLogBook()
    {
        return View();
    }

    [Route("at2.1-master-schedule")]
    public IActionResult AT21AuditMasterSchedule()
    {
        return View();
    }

    [Route("at2.2")]
    public IActionResult AT22AuditBacklogMasterShceduleForAllYears()
    {
        return View();
    }

    [Route("at3.2")]
    public IActionResult AT32AuditWorkDoneWIP()
    {
        return View();
    }

    [Route("at4.1")]
    public IActionResult AEX41AuditOTorAEXSchedulingForCurrentYear()
    {
        return View();
    }

    [Route("at4.2")]
    public IActionResult AEX42AuditOTorAEXBackLog()
    {
        return View();
    }
}
