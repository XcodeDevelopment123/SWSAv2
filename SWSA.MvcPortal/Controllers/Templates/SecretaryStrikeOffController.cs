using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Templates;
[Route("secretary-dept/strike-off-template")]
public class SecretaryStrikeOffController(
   AppDbContext db
    ) : BaseController
{

    private readonly DbSet<SecStrikeOffTemplate> _strikeOffs = db.Set<SecStrikeOffTemplate>();

    [Route("")]
    public async Task<IActionResult> StrikeOffTemplate()
    {
        var data = await _strikeOffs
            .Include(c => c.Client)
            .Include(c => c.DoneBy)
            .ToListAsync();
        return View(data);
    }

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var task = await _strikeOffs.Where(c => c.Id == id)
            .Include(c=>c.DoneBy)
            .Include(c => c.Client)
            .FirstOrDefaultAsync();
        return Ok(task);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(SecStrikeOffTemplate req)
    {
        _strikeOffs.Add(req);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = req.Id });
    }

    [InternalAjaxOnly]
    [HttpPost("{id:int}/update")]
    public async Task<IActionResult> Update([FromRoute] int id, SecStrikeOffTemplate req)
    {
        var task = await _strikeOffs.FirstOrDefaultAsync(c => c.Id == id);
        if (task == null)
        {
            return Ok();
        }

        task.StartDate = req.StartDate;
        task.CompleteDate = req.CompleteDate;
        task.DoneByUserId = req.DoneByUserId;
        task.PenaltiesAmount = req.PenaltiesAmount;
        task.RevisedPenaltiesAmount = req.RevisedPenaltiesAmount;
        task.SSMPenaltiesAppealDate = req.SSMPenaltiesAppealDate;
        task.SSMPenaltiesPaymentDate = req.SSMPenaltiesPaymentDate;
        task.SSMDocSentDate = req.SSMDocSentDate;
        task.SSMSubmissionDate = req.SSMSubmissionDate;
        task.Remarks = req.Remarks;

        _strikeOffs.Update(task);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = task.Id });
    }

    [InternalAjaxOnly]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var task = await _strikeOffs.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        _strikeOffs.Remove(task);
        await db.SaveChangesAsync();

        return Ok();
    }

    #endregion
}
