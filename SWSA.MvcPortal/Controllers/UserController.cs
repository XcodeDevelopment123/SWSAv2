using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

public class UserController() : BaseController
{

    public IActionResult UserList()
    {
        return View();
    }
}
