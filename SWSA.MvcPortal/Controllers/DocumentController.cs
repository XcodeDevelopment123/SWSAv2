using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[Route("companies")]
public class DocumentController(
    IDocumentRecordService service
    ) : BaseController
{
    [Route("docs")]
    public async Task<IActionResult> List()
    {
        return View();
    }

    [Route("docs/create")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
}
