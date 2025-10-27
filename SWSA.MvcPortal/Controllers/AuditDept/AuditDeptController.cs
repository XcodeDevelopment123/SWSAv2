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

        public IActionResult AuditMasterSchedule()
        {
            return View();
        }

        public IActionResult AuditMasterLogBook()
        {
            return View();
        }

        public IActionResult AT32Audit()
        {
            return View();
        }
        public IActionResult AT33Audit()
        {
            return View();
        }
        public IActionResult AT34Audit()
        {
            return View();
        }
    }
}
