using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Models.CompnayWorks;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyWorkController(
    ICompanyWorkAssignmentService service,
    ICompanyWorkProgressService progressService,
    IUserService userService,
    ICompanyService companyService
    ) : BaseController
{
    [Route("works")]
    public async Task<IActionResult> List()
    {
        var works = await service.GetWorkAssignments();
        return View(works);
    }

    [Route("works/overview")]
    public async Task<IActionResult> Overview()
    {
        var works = await service.GetWorkAssignments();
        return View(works);
    }

    [Route("{companyId}/works/create")]
    public async Task<IActionResult> Create([FromRoute] int companyId)
    {
        var cp = await companyService.GetCompanyByIdAsync(companyId);
        var users = await userService.GetUserSelectionByCompanyIdAsync(companyId);
        var vm = new CompanyWorkAssignmentCreatePageVM(cp, users);
        return View(vm);
    }


    [Route("works/{taskId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int taskId)
    {
        var data = await service.GetWorkAssignmentById(taskId);
        if (data.Company != null)
            data.Company = await companyService.GetCompanyByIdAsync(data.Company.Id);

        var users = await userService.GetUserSelectionByCompanyIdAsync(data.Company.Id);
        var vm = new CompanyWorkAssignmentkEditPageVM(data, users);
        return View(vm);
    }

    [Route("works/calendar-events")]
    [HttpGet]
    public async Task<IActionResult> GetCalendarEvent()
    {
        var data = await service.GetWorkCalendarEvents();
        return Json(data);
    }

    [Route("works/create")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyWorkAssignmentRequest req)
    {
        var result = await service.CreateCompanyWorkAssignment(req);
        return Json(result);
    }

    [Route("works/edit")]
    [HttpPost]
    public async Task<IActionResult> Edit(EditCompanyWorkAssignmentRequest req)
    {
        var result = await service.EditCompanyWorkAssignment(req);
        return Json(result);
    }


    [Route("works/progress/edit")]
    [HttpPost]
    public async Task<IActionResult> EditProgress(EditCompanyWorkProgressRequest req)
    {
        var result = await progressService.EditCompanyWorkProgress(req);
        return Json(result);
    }

    [Route("works/{taskId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int taskId)
    {
        var result = await service.DeleteWork(taskId);
        return Json(result);
    }

}
