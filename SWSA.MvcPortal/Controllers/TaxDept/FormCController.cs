using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers.TaxDept
{
    public class FormCController : Controller
    {
        public IActionResult FormC()
        {
            return View();
        }

        public IActionResult TaxDeptWorkSchedule()
        {
            return View();
        }

        public IActionResult TaxWorkLogbook()
        {
            return View();
        }

        public IActionResult StrikeOffTaxWork()
        {
            return View();
        }

    }
}
