using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers.AccDept
{
    public class AccountDeptController : Controller
    {
        public IActionResult SdnBhdMasterScheduleList()
        {
            return View();
        }

        public IActionResult LLPMasterScheduleList()
        {
            return View();
        }

        public IActionResult FormBnPMasterScheduleList()
        {
            return View();
        }

        public IActionResult ClientAccAuditMasterSchedulingList()
        {
            return View();
        }

        public IActionResult IndividuaFormBEMasterSchedulingList()
        {
            return View();
        }

        public IActionResult FormEMasterSchedule()
        {
            return View();
        }
        public IActionResult SdnBhdBacklog()
        {
            return View();
        }
        public IActionResult LLPBacklog()
        {
            return View();
        }
        public IActionResult FormBnPBacklog()
        {
            return View();
        }
    }
}
