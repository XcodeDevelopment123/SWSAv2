using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Models.ScheduledJobs;

namespace SWSA.MvcPortal.ViewComponents;

public class ScheduleJobUpdateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ScheduledJobVM vm)
    {
        return View(vm);
    }
}
