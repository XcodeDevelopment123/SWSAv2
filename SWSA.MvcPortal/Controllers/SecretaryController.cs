using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Submission;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;
using SWSA.MvcPortal.Services.Interfaces.Submission;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyService companyService,
    ICompanyStrikeOffSubmissionService cpStrikeOffSubmissionService,
    IAuditSubmissionService auditSubmissionService,
    ILLPSubmissionService llpSubmissionService,
    IAnnualReturnSubmissionService arSubmissionService
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

    [Route("submissions/llp")]
    public async Task<IActionResult> LLPSubmissionList()
    {
        var data = await llpSubmissionService.GetLLPSubmissionVMsAsync();
        return View(data);
    }

    [Route("submissions/llp/{submissionId}/edit")]
    public async Task<IActionResult> EditLLPSubmission([FromRoute] int submissionId)
    {
        var data = await llpSubmissionService.GetLLPSubmissionVMByIdAsync(submissionId);
        return View(data);
    }

    [Route("submissions/annual-return")]
    public async Task<IActionResult> ARSubmissionList()
    {
        var data = await arSubmissionService.GetARSubmissionVMsAsync();
        return View(data);
    }

    [Route("submissions/annual-return/{submissionId}/edit")]
    public async Task<IActionResult> EditARSubmission([FromRoute] int submissionId)
    {
        var data = await arSubmissionService.GetARSubmissionVMByIdAsync(submissionId);
        return View(data);
    }
    #endregion


    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("submissions/create")]
    public async Task<IActionResult> RequestSubmission(int companyId, WorkType type)
    {
        var result = type switch
        {
            WorkType.LLP => await llpSubmissionService.RequestSubmissionForCompany(companyId),
            WorkType.Audit => await auditSubmissionService.RequestSubmissionForCompany(companyId),
            WorkType.StrikeOff => await cpStrikeOffSubmissionService.RequestSubmissionForCompany(companyId),
            WorkType.AnnualReturn => await arSubmissionService.RequestSubmissionForCompany(companyId),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid work type")
        };
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("submissions/annual-return/update")]
    public async Task<IActionResult> UpdateARSubmission(EditARSubmissionRequest req)
    {
        var result = await arSubmissionService.UpdateSubmissionForCompany(req);
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
    [HttpPost("submissions/company-strike-off/update")]
    public async Task<IActionResult> UpdateStrikeOffSubmission(EditCompanyStrikeOffSubmissionRequest req)
    {
        var result = await cpStrikeOffSubmissionService.UpdateSubmissionForCompany(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("submissions/llp/update")]
    public async Task<IActionResult> UpdateLLPSubmission(EditLLPSubmissionRequest req)
    {
        var result = await llpSubmissionService.UpdateSubmissionForCompany(req);
        return Ok(result);
    }


    #endregion
}
