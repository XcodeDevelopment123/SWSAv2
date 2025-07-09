using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities.WorkAssignments;
using SWSA.MvcPortal.Models.CompnayWorks;
using SWSA.MvcPortal.Services.Interfaces.CompanyProfile;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignment;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyWorkController(
    ICompanyWorkAssignmentService service,
    ICompanyWorkProgressService progressService,
    IUserService userService,
    IWorkAssignmentUserMappingService workUserService,
    ICompanyService companyService
    ) : BaseController
{
    #region Page/View
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
        var cpInfo = await companyService.GetCompanySimpleInfoVMByIdAsync(data.CompanyId);
        var users = await userService.GetUserSelectionByCompanyIdAsync(data.CompanyId);
        var vm = new CompanyWorkAssignmentkEditPageVM(data, cpInfo, users);
        return View(vm);
    }

    [Route("works/audit/{taskId}/edit")]
    public async Task<IActionResult> AuditEdit([FromRoute] int taskId)
    {
        var data = await service.GetWorkAssignmentById<AuditWorkAssignment>(taskId);
        return View(data);
    }

    [Route("works/annual-return/{taskId}/edit")]
    public async Task<IActionResult> AnnualReturnEdit([FromRoute] int taskId)
    {
        var data = await service.GetWorkAssignmentById<AnnualReturnWorkAssignment>(taskId);
        return View(data);
    }

    [Route("works/strike-off/{taskId}/edit")]
    public async Task<IActionResult> StrikeOffEdit([FromRoute] int taskId)
    {
        var data = await service.GetWorkAssignmentById<StrikeOffWorkAssignment>(taskId);
        return View(data);
    }

    [Route("works/llp/{taskId}/edit")]
    public async Task<IActionResult> LLPEdit([FromRoute] int taskId)
    {
        var data = await service.GetWorkAssignmentById<LLPWorkAssignment>(taskId);
        return View(data);
    }
    #endregion

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("works/calendar-events")]
    public async Task<IActionResult> GetCalendarEvent()
    {
        var data = await service.GetWorkCalendarEvents();
        return Ok(data);
    }

    [InternalAjaxOnly]
    [HttpPost("works/create")]
    public async Task<IActionResult> Create(CreateCompanyWorkAssignmentRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/update")]
    public async Task<IActionResult> Edit(EditCompanyWorkAssignmentRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/add-user")]
    public async Task<IActionResult> AddWorkUser(WorkUserRequest req)
    {
        int result = await workUserService.AddUser(req);
        return Ok(result);
    }


    [InternalAjaxOnly]
    [HttpPost("works/remove-user")]
    public async Task<IActionResult> DeleteWorkUser(WorkUserRequest req)
    {
        bool result = await workUserService.RemoveUser(req);
        return Ok(result);
    }


    [InternalAjaxOnly]
    [HttpPost("works/progress/edit")]
    public async Task<IActionResult> EditProgress(EditCompanyWorkProgressRequest req)
    {
        var result = await progressService.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("works/{taskId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int taskId)
    {
        var result = await service.Delete(taskId);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/annual-return/update")]
    public async Task<IActionResult> UpdateARWork(EditARWorkRequest req)
    {
        var result = await service.UpdateWork<EditARWorkRequest, AnnualReturnWorkAssignment>(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/audit-report/update")]
    public async Task<IActionResult> UpdateAuditWork(EditAuditWorkRequest req)
    {
        var result = await service.UpdateWork<EditAuditWorkRequest, AuditWorkAssignment>(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/strike-off/update")]
    public async Task<IActionResult> UpdateStrikeOffWork(EditStrikeOffWorkRequest req)
    {
        var result = await service.UpdateWork<EditStrikeOffWorkRequest, StrikeOffWorkAssignment>(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("works/llp/update")]
    public async Task<IActionResult> UpdateLLPWork(EditLLPWorkRequest req)
    {
        var result = await service.UpdateWork<EditLLPWorkRequest, LLPWorkAssignment>(req);
        return Ok(result);
    }


    #endregion
}
