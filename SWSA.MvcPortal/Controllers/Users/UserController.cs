using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;

namespace SWSA.MvcPortal.Controllers.Users;

[Route("users")]
public class UserController(
    IUserService service
    ) : BaseController
{
    #region Page/View
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetUsersAsync();
        return View(data);
    }

    [Route("{staffId}/overview")]
    public async Task<IActionResult> Overview([FromRoute] string staffId)
    {
        var data = await service.GetUserOverviewVMAsync(staffId);
        return View(data);
    }

    [Route("create")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("{staffId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] string staffId)
    {
        var data = await service.GetUserByIdAsync(staffId);
        return View(data);
    }
    #endregion

    #region API/Ajax
    [InternalAjaxOnly]
    [HttpGet("pic")]
    public async Task<IActionResult> GetPicVM()
    {
        var result = await service.GetUserCardVMAsync();
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateUserRequest req)
    {
        var result = await service.Create(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("edit")]
    public async Task<IActionResult> Edit(EditUserRequest req)
    {
        var result = await service.Edit(req);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("{staffId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] string staffId)
    {
        var result = await service.DeleteUserByIdAsync(staffId);
        return Ok(result);
    }
    #endregion
}
