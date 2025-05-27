using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Submission;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;
using SWSA.MvcPortal.Services.Interfaces.Submission;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyService companyService,
    ICompanyStrikeOffSubmissionService cpStrikeOffSubmissionService,
    IAuditSubmissionService auditSubmissionService
    ) : BaseController
{
    #region Page/View
    [Route("companies")]
    public async Task<IActionResult> CompanyList()
    {
        var data = await companyService.GetCompanySelectionAsync();
        return View(data);
    }

    [Route("submissions/audit-report")]
    public async Task<IActionResult> AuditRepotSubmissionList()
    {
        var data = await auditSubmissionService.GetAuditSubmissionVMsAsync();
        return View(data);
    }

    [Route("submissions/audit-report/{submissionId}/edit")]
    public async Task<IActionResult> EditAuditRepotSubmission([FromRoute] int submissionId)
    {
        var data = await auditSubmissionService.GetAuditSubmissionVMByIdAsync(submissionId);
        return View(data);
    }


    [Route("submissions/companies-strike-off")]
    public async Task<IActionResult> CompanyStrikeOffList()
    {
        var data = await cpStrikeOffSubmissionService.GetStrikeOffSubmissionVMsAsync();
        return View(data);
    }

    [Route("submissions/company-strike-off/{submissionId}/edit")]
    public async Task<IActionResult> EditCompanyStrikeOffSubmission([FromRoute] int submissionId)
    {
        var data = await cpStrikeOffSubmissionService.GetStrikeOffSubmissionVMByIdAsync(submissionId);
        return View(data);
    }
    #endregion

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("submissions/audit-report/create")]
    public async Task<IActionResult> RequestAuditSubmission(int companyId)
    {
        var result = await auditSubmissionService.RequestSubmissionForCompany(companyId);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("submissions/audit-report/update")]
    public async Task<IActionResult> UpdateAuditSubmission(EditAuditSubmissionRequest req)
    {
        var result = await auditSubmissionService.UpdateSubmissionForCompany(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("submissions/company-strike-off/create")]
    public async Task<IActionResult> RequestStrikeOffSubmission(int companyId)
    {
        var result = await cpStrikeOffSubmissionService.RequestSubmissionForCompany(companyId);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("submissions/company-strike-off/update")]
    public async Task<IActionResult> UpdateStrikeOffSubmission(EditCompanyStrikeOffSubmissionRequest req)
    {
        var result = await cpStrikeOffSubmissionService.UpdateSubmissionForCompany(req);
        return Ok(result);
    }

    #endregion
}
