using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

namespace SWSA.MvcPortal.Controllers;

[Route("secretary-dept")]
public class SecretaryController(
    ICompanyWorkAssignmentService companyWorkAssignmentService
    ) : BaseController
{
    #region Page/View
    [Route("landing")]
    public IActionResult Landing()
    {
        return View();
    }

    [Route("companies")]
    public async Task<IActionResult> CompanyList()
    {
  //      var data = await companyService.GetCompanySelectionAsync();
        return View();
    }

    [Route("companies/sdn-bhd")]
    public async Task<IActionResult> SdnBhdCompanyList()
    {
        return View();
    }

    [Route("companies/llp")]
    public async Task<IActionResult> LLPCompanyList()
    {
        return View();
    }

    //[Route("submissions/audit-report")]
    //public async Task<IActionResult> AuditRepotSubmissionList()
    //{
    //    var data = await auditSubmissionService.GetAuditSubmissionVMsAsync();
    //    return View(data);
    //}

    //[Route("submissions/companies-strike-off")]
    //public async Task<IActionResult> CompanyStrikeOffList()
    //{
    //    var data = await cpStrikeOffSubmissionService.GetStrikeOffSubmissionVMsAsync();
    //    return View(data);
    //}

    //[Route("submissions/llp")]
    //public async Task<IActionResult> LLPSubmissionList()
    //{
    //    var data = await llpSubmissionService.GetLLPSubmissionVMsAsync();
    //    return View(data);
    //}

    //[Route("submissions/annual-return")]
    //public async Task<IActionResult> ARSubmissionList()
    //{
    //    var data = await arSubmissionService.GetARSubmissionVMsAsync();
    //    return View(data);
    //}

    #endregion

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("submissions/create")]
    public async Task<IActionResult> RequestSubmission(WorkAssignmentRequest req)
    {
        var result = await companyWorkAssignmentService.RequestWorkAssignment(req);
        return Ok(AppUrlHelper.GenerateWorkEditUrl(req.Type, result));
    }
    #endregion
}
