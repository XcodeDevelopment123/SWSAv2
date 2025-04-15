using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[AllowAnonymous]
public class AuthController(IAuthService service, IUserService userService) : BaseController
{
    public IActionResult Login()
    {
        return View();
    }

    [Route("/auth/partner-login")]
    public IActionResult PartnerLogin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var result = await service.Login(username, password);
        return Json(result);
    }

    [HttpPost("/auth/login-partner")]
    public async Task<IActionResult> PartnerLogin(string username, string password)
    {
        var result = await service.PartnerLogin(username, password);
        return Json(result);
    }


    public IActionResult Logout()
    {
        service.Logout();
        return RedirectToAction("Login");
    }
}
