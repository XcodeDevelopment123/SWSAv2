using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.CompanyStrikeOffSubmissions;
using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;
using SWSA.MvcPortal.Services.Interfaces.Submission;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyService companyService,
    ICompanyStrikeOffSubmissionService cpStrikeOffSubmissionService
    ) : BaseController
{
    #region Page/View
    [Route("companies")]
    public async Task<IActionResult> CompanyList()
    {
        var data = await companyService.GetCompanySelectionAsync();
        return View(data);
    }

    [Route("companies-strike-off")]
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
