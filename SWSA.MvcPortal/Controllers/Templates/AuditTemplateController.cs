using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.Templates;

[Route("audit-templates")]
public class AuditTemplateController(
   AppDbContext db
    ) : BaseController
{
    private readonly DbSet<AuditTemplate> _auditTemplates = db.Set<AuditTemplate>();

    [Route("")]
    public async Task<IActionResult> AuditTemplate()
    {
        var data = await _auditTemplates
            .Include(c => c.Client)
            .Include(c => c.PersonInCharge)
            .ToListAsync();
        return View(data);
    }

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var task = await _auditTemplates.Where(c => c.Id == id)
            .Include(c => c.PersonInCharge)
            .Include(c => c.Client)
            .FirstOrDefaultAsync();
        return Ok(task);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(AuditTemplate req)
    {
        _auditTemplates.Add(req);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = req.Id });
    }

    [InternalAjaxOnly]
    [HttpPost("{id:int}/update")]
    public async Task<IActionResult> Update([FromRoute] int id, AuditTemplate req)
    {
        var task = await _auditTemplates.FirstOrDefaultAsync(c => c.Id == id);
        if (task == null)
        {
            return Ok();
        }

        task.PersonInChargeId = req.PersonInChargeId;
        task.Database = req.Database;
        task.Status = req.Status;
        task.QuarterToDo = req.QuarterToDo;
        task.YearEndToDo = req.YearEndToDo;

        task.Revenue = req.Revenue;
        task.Profit = req.Profit;
        task.AuditFee = req.AuditFee;
        task.DateBilled = req.DateBilled;

        task.AuditStartDate = req.AuditStartDate;
        task.AuditEndDate = req.AuditEndDate;
        task.TotalFieldWorkDays = req.TotalFieldWorkDays;
        task.AuditWIPResult = req.AuditWIPResult;

        task.IsAccSetupComplete = req.IsAccSetupComplete;
        task.IsAccSummaryComplete = req.IsAccSummaryComplete;
        task.IsAuditPlanningComplete = req.IsAuditPlanningComplete;
        task.IsAuditExecutionComplete = req.IsAuditExecutionComplete;
        task.IsExecutionAuditComplete = req.IsExecutionAuditComplete;

        task.FirstReviewSendDate = req.FirstReviewSendDate;
        task.FirstReviewEndDate = req.FirstReviewEndDate;
        task.FirstReviewResult = req.FirstReviewResult;
        task.SecondReviewSendDate = req.SecondReviewSendDate;
        task.SecondReviewEndDate = req.SecondReviewEndDate;
        task.SecondReviewResult = req.SecondReviewResult;

        task.KualaLumpurOfficeDateSent = req.KualaLumpurOfficeDateSent;
        task.KualaLumpurOfficeAuditReportReceivedDate = req.KualaLumpurOfficeAuditReportReceivedDate;
        task.KualaLumpurOfficeReportDate = req.KualaLumpurOfficeReportDate;
        task.KualaLumpurOfficeDirectorsReportDate = req.KualaLumpurOfficeDirectorsReportDate;

        task.DirectorDateSent = req.DirectorDateSent;
        task.DirectorFollowUpDate = req.DirectorFollowUpDate;
        task.DirectorDateReceived = req.DirectorDateReceived;
        task.DirectorCommOfOathsDate = req.DirectorCommOfOathsDate;

        task.TaxDueDate = req.TaxDueDate;
        task.DatePassToTaxDept = req.DatePassToTaxDept;

        task.SecSSMDueDate = req.SecSSMDueDate;
        task.DatePassToSecDept = req.DatePassToSecDept;

        task.PostAuditDateBinded = req.PostAuditDateBinded;
        task.PostAuditDespatchDateToClient = req.PostAuditDespatchDateToClient;

        _auditTemplates.Update(task);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(GetById), new { id = task.Id });
    }

    [InternalAjaxOnly]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var task = await _auditTemplates.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        _auditTemplates.Remove(task);
        await db.SaveChangesAsync();

        return Ok();
    }

    #endregion
}
