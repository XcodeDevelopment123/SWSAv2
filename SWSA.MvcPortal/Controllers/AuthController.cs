using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[AllowAnonymous]
public class AuthController(
    IAuthService service
    ) : BaseController
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

    [HttpGet("keep-alive")]
    public IActionResult KeepAlive()
    {
        return NoContent();
    }

    public IActionResult Logout()
    {
        service.Logout();
        return RedirectToAction("Login");
    }
}
