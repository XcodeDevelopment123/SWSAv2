using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Backlogs;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Backlogs;

[Route("backlogs/audit-master")]
public class AuditLogScheduleController(
   AppDbContext db
    ) : BaseController
{
    private readonly DbSet<AuditBacklogSchedule> _backlogs = db.Set<AuditBacklogSchedule>();

    [Route("")]
    public async Task<IActionResult> AuditLogMasterSchedule()
    {
        var data = await _backlogs
                  .Include(c => c.Client)
                  .ToListAsync();
        return View(data);
    }

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var task = await _backlogs.Where(c => c.Id == id)
            .Include(c => c.Client)
            .FirstOrDefaultAsync();
        return Ok(task);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(AuditBacklogSchedule req)
    {
        _backlogs.Add(req);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = req.Id });
    }

    [InternalAjaxOnly]
    [HttpPost("{id:int}/update")]
    public async Task<IActionResult> Update([FromRoute] int id, AuditBacklogSchedule req)
    {
        var log = await _backlogs.FirstOrDefaultAsync(c => c.Id == id);
        if (log == null)
        {
            return Ok();
        }

        log.ClientId = req.ClientId;
        log.QuarterToDoAudit = req.QuarterToDoAudit;
        log.YearToDo = req.YearToDo;    
        log.ReasonForBacklog = req.ReasonForBacklog;

        _backlogs.Update(log);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = log.Id });
    }

    [InternalAjaxOnly]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var log = await _backlogs.FindAsync(id);
        if (log == null)
        {
            return NotFound();
        }
        _backlogs.Remove(log);
        await db.SaveChangesAsync();

        return Ok();
    }

    #endregion
}
