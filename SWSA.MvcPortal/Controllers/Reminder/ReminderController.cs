using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers.Reminder
{
    public class ReminderController : Controller
    {
        public IActionResult SdnBhd18monthReminder()
        {
            return View();
        }
        public IActionResult AuditReminderSchedule()
        {
            return View();
        }
        public IActionResult SdnBhdAccDocReminderSchedule()
        {
            return View();
        }

        public IActionResult LLPAccDocReminderSchedule()
        {
            return View();
        }

        public IActionResult FormBnPReminderSchedule()
        {
            return View();
        }

        public IActionResult FormEReminderSchedule()
        {
            return View();
        }
        public IActionResult FormBEindividualTaxReminderSchedule()
        {
            return View();
        }
    }
}
