using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Models.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[Route("companies")]
public class DocumentController(
    IDocumentRecordService service,
    ICompanyDepartmentService companyDepartmentService,
    IUserService userService,
    ICompanyService companyService
    ) : BaseController
{
    [Route("docs")]
    public async Task<IActionResult> List()
    {
        var documents = await service.GetDocumentRecords();
        return View(documents);
    }

    [Route("{companyId}/docs/create")]
    public async Task<IActionResult> Create([FromRoute] int companyId)
    {
        var cp = await companyService.GetCompanyByIdAsync(companyId);
        var dpts = cp.Departments.ToList();
        var staff = await userService.GetUserSelectionAsync();
        var vm = new DocumentRecordCreatePageVM(cp, dpts, staff);
        return View(vm);
    }
}
