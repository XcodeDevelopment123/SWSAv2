using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers.AuditDept
{
    public class AuditDeptController : Controller
    {
        public IActionResult AuditTemplate()
        {
            return View();
        }
        public IActionResult AuditBacklog()
        {
            return View();
        }
        public IActionResult AuditAexTemplate()
        {
            return View();
        }
        public IActionResult AuditAexBacklog()
        {
            return View();
        }
    }
}
