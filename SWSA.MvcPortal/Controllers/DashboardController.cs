using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers;

[Route("dashboard")]
public class DashboardController : BaseController
{
    #region Page/View
    [Route("overall")]
    public IActionResult Overall()
    {
        return View();
    }

    public IActionResult AccDeptDashboard() //Account
    {
        return View();
    }

    public IActionResult SecDeptDashboard() //Secretary
    {
        return View();
    }

    public IActionResult TaxDeptDashboard() //Tax
    {
        return View();
    }

    public IActionResult AuditDeptDashboard() //Audit
    {
        return View();
    }
    #endregion
}
