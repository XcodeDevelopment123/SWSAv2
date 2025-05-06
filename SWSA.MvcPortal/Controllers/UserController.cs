using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("users")]
public class UserController(
    IUserService service
    ) : BaseController
{

    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetUsersAsync();
        return View(data);
    }

    [Route("{staffId}/overview")]
    public async Task<IActionResult> Overview([FromRoute] string staffId)
    {
        var data = await service.GetUserByIdAsync(staffId);
        return View(data);
    }

    [Route("create")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest req)
    {
        var result = await service.CreateUser(req);
        return Json(result);
    }

    [Route("{staffId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] string staffId)
    {
        var data = await service.GetUserByIdAsync(staffId);
        return View(data);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> Edit(EditUserRequest req)
    {
        var result = await service.UpdateUserInfo(req);
        return Json(result);
    }

    [HttpDelete("{staffId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] string staffId)
    {
        var result = await service.DeleteUserByIdAsync(staffId);
        return Json(result);
    }
}
