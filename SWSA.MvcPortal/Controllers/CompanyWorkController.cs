using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyWorkController(
    ICompanyWorkAssignmentService service,
    ICompanyWorkProgressService progressService
    ) : BaseController
{
    [Route("works")]
    public async Task<IActionResult> List()
    {
        var works = await service.GetWorkAssignments();
        return View(works);
    }

    [Route("works/create")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
}
