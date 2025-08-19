using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Templates;

[Route("templates/tax-strike-off")]
public class TaxStrikeOffTemplateController(
   AppDbContext db
    ) : BaseController
{

    private readonly DbSet<TaxStrikeOffTemplate> _strikeOffs = db.Set<TaxStrikeOffTemplate>();

    [Route("")]
    public async Task<IActionResult> TaxStrikeOffTemplate()
    {
        var data = await _strikeOffs
            .Include(c => c.Client)
            .ToListAsync();
        return View(data);
    }

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var task = await _strikeOffs.Where(c => c.Id == id)
            .Include(c => c.Client)
            .FirstOrDefaultAsync();
        return Ok(task);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(TaxStrikeOffTemplate req)
    {
        _strikeOffs.Add(req);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = req.Id });
    }

    [InternalAjaxOnly]
    [HttpPost("{id:int}/update")]
    public async Task<IActionResult> Update([FromRoute] int id, TaxStrikeOffTemplate req)
    {
        var task = await _strikeOffs.FirstOrDefaultAsync(c => c.Id == id);
        if (task == null)
        {
            return Ok();
        }

        task.IRBPenaltiesAmount = req.IRBPenaltiesAmount;
        task.PenaltiesAppealDate = req.PenaltiesAppealDate;
        task.PenaltiesPaymentDate = req.PenaltiesPaymentDate;
        task.IsAccountWorkComplete = req.IsAccountWorkComplete;
        task.FormESubmitDate = req.FormESubmitDate;
        task.FormCSubmitDate = req.FormCSubmitDate;
        task.InvoiceDate = req.InvoiceDate;
        task.InvoiceAmount = req.InvoiceAmount;
        task.IsClientCopySent = req.IsClientCopySent;
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
