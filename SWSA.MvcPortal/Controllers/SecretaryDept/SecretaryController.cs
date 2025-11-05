using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.SecretaryDept;

[Route("secretary-dept")]
public class SecretaryController(
       AppDbContext db
    ) : BaseController
{
    private readonly DbSet<SecDeptTaskTemplate> _tasks = db.Set<SecDeptTaskTemplate>();

    #region Page/View
    [Route("landing")]
    public async Task<IActionResult> Landing()
    {
        var data = await _tasks.Include(c => c.Client).ToListAsync();
        return View(data);
    }

    [Route("mastersec")]
    public async Task<IActionResult> SdnBhdMasterSecSchedule()
    {
        return View();
    }

    [Route("ARSubmisionReport")]
    public async Task<IActionResult> ARSubmisionReport()
    {
        return View();
    }
    [Route("AASR")]
    public async Task<IActionResult> AuditedAccSubmissionReport()
    {
        return View();
    }

    [Route("LLPSR")]
    public async Task<IActionResult> LLPSubmissionReport()
    {
        return View();
    }

    [Route("StrikeOffSchedule")]
    public async Task<IActionResult> StrikeOffSchedule()
    {
        return View();
    }

    #endregion

    #region API/Ajax

    #endregion
}
