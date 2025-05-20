using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Services.UploadFile;
using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;
using SWSA.MvcPortal.Models.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;


[Route("companies")]
public class DocumentController(
    IDocumentRecordService service,
    IUserService userService,
    ICompanyService companyService
    ) : BaseController
{
    [Route("docs")]
    public async Task<IActionResult> List()
    {
        var documents = await service.GetDocumentRecords();
        return View(documents);
    }

    [Route("{companyId}/docs/create")]
    public async Task<IActionResult> Create([FromRoute] int companyId)
    {
        var cp = await companyService.GetCompanyByIdAsync(companyId);
        var staff = await userService.GetUserSelectionByCompanyIdAsync(companyId);
        var vm = new DocumentRecordCreatePageVM(cp, staff);
        return View(vm);
    }

    [Route("docs/create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDocumentRecordListRequest req, List<IFormFile> files)
    {
        await service.CreateDocuments(req, files);

        return Json(true);
    }

    [Route("docs/{docId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int docId)
    {
        var result = await service.DeleteDocumentById(docId);
        return Json(true);
    } 
}
