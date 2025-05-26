using Microsoft.AspNetCore.Mvc;
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
    #region Page/View
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
    #endregion


    #region API/Ajax
    [InternalAjaxOnly]
    [HttpPost("docs/create-work")]
    public async Task<IActionResult> CreateDocuments([FromForm] DocumentRecordRequest req, IFormFile file)
    {
        var result = await service.CreateDocument(req, file);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpPost("docs/create")]
    public async Task<IActionResult> CreateDocuments([FromForm] DocumentRecordListRequest req, List<IFormFile> files)
    {
        var result = await service.CreateDocuments(req, files);
        return Ok(result);
    }

    [InternalAjaxOnly]
    [HttpDelete("docs/{docId}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int docId)
    {
        var result = await service.Delete(docId);
        return Ok(result);
    }
    #endregion
}
