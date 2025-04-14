using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Models.CompnayWorks;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyWorkController(
    ICompanyWorkAssignmentService service,
    ICompanyWorkProgressService progressService,
    ICompanyService companyService
    ) : BaseController
{
    [Route("works")]
    public async Task<IActionResult> List()
    {
        var works = await service.GetWorkAssignments();
        return View(works);
    }

    [Route("{companyId}/works/create")]
    public async Task<IActionResult> Create([FromRoute] int companyId)
    {
        var cp = await companyService.GetCompanyByIdAsync(companyId);
        var vm = new CompanyWorkAssignmentCreatePageVM(cp);
        return View(vm);
    }


    [Route("works/create")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyWorkAssignmentRequest req)
    {
        var result = await service.CreateCompanyWorkAssignment(req);
        return Json(result);
    }

}
