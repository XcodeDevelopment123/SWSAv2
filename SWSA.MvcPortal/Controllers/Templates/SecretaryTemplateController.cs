using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Templates;

[Route("secretary-dept/template")]
public class SecretaryTemplateController(
   AppDbContext db
    ) : BaseController
{
    private readonly DbSet<SecDeptTaskTemplate> _tasks = db.Set<SecDeptTaskTemplate>(); 

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var task = await _tasks.Where(c => c.Id == id).Include(c => c.Client).FirstOrDefaultAsync();
        return Ok(task);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(SecDeptTaskTemplate req)
    {
        _tasks.Add(req);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = req.Id });
    }

    [InternalAjaxOnly]
    [HttpPost("{id:int}/update")]
    public async Task<IActionResult> Update([FromRoute] int id, SecDeptTaskTemplate req)
    {
        var task = await _tasks.Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == id);
        if (task == null)
        {
            return Ok();
        }

        task.ADSubmitDate = req.ADSubmitDate;
        task.ADSendToClientDate = req.ADSendToClientDate;
        task.ADReturnByClientDate = req.ADReturnByClientDate;
        task.ARDueDate = req.ARDueDate;
        task.ARReturnByClientDate = req.ARReturnByClientDate;
        task.ARSendToClientDate = req.ARSendToClientDate;
        task.ARSubmitDate = req.ARSubmitDate;
        task.Remarks = req.Remarks;

        _tasks.Update(task);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = task.Id });
    }

    [InternalAjaxOnly]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var task = await _tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        _tasks.Remove(task);
        await db.SaveChangesAsync();

        return Ok();
    }

    #endregion
}
