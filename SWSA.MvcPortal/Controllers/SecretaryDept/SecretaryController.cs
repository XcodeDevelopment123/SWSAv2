using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Commons.Helpers;
using SWSA.MvcPortal.Entities.SecretaryDept;
using SWSA.MvcPortal.Persistence;
using System.Threading.Tasks;

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

    #endregion
}
