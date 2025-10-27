using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers.Templates
{
    public class SecreataryTaskTemplateController : Controller
    {
        public IActionResult SecDeptTaskTemplate()
        {
            ViewData["controller"] = "SecreataryTaskTemplate";
            ViewData["action"] = "SecDeptTaskTemplate";

            return View();
        }
    }
}
