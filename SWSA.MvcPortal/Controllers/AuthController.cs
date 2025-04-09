using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[AllowAnonymous]
public class AuthController(IAuthService service,IUserService userService) : BaseController
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var result = await service.Login(username, password);
        return Json(result);
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login");
    }
}
