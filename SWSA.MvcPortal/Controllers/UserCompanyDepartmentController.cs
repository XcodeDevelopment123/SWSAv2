using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;

namespace SWSA.MvcPortal.Controllers;

[Route("companies/{companyId}/handle-users")]
public class UserCompanyDepartmentController
    (IUserCompanyDepartmentService service)
    : BaseController
{
    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromRoute] int companyId, CreateUserCompanyDepartmentRequest req)
    {
        var result = await service.Create(companyId, req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("update")]
    public async Task<IActionResult> Edit([FromRoute] int companyId, EditUserCompanyDepartment req)
    {
        var result = await service.Edit(companyId, req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{staffId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int companyId, [FromRoute] string staffId)
    {
        var result = await service.Delete(companyId, staffId);
        return Ok(result);
    }
    #endregion
}
