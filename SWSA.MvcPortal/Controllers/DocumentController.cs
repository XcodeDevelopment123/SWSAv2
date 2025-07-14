using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;
using SWSA.MvcPortal.Models.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;
using SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

namespace SWSA.MvcPortal.Controllers;


[Route("documents")]
public class DocumentController(
    IDocumentRecordService service,
    IUserService userService
    ) : BaseController
{
    //#region Page/View
    //[Route("audit-dept")]
    //public async Task<IActionResult> AuditDepartment()
    //{
    //    var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Audit);
    //    var companies = await companyService.GetCompanySelectionAsync();
    //    var vm = new DocumentRecordAuditDeptPageVM(companies, documents);
    //    return View(vm);
    //}

    //[Route("account-dept")]
    //public async Task<IActionResult> AccountDepartment()
    //{
    //    var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Account);
    //    return View(documents);
    //}

    //[Route("docs")]
    //public async Task<IActionResult> List()
    //{
    //    var documents = await service.GetDocumentRecords();
    //    return View(documents);
    //}

    //[Route("{companyId}/docs/create")]
    //public async Task<IActionResult> Create([FromRoute] int companyId)
    //{
    //    var cp = await companyService.GetCompanySimpleInfoVMByIdAsync(companyId);
    //    var staff = await userService.GetUserSelectionAsync();
    //    var vm = new DocumentRecordCreatePageVM(cp, staff);
    //    return View(vm);
    //}
    //#endregion


    //#region API/Ajax
    //[InternalAjaxOnly]
    //[HttpPost("submit-doc-record")]
    //public async Task<IActionResult> CreateDocument(DocumentRecordRequest req)
    //{
    //    var result = await service.CreateDocument(req);
    //    return Ok(result);
    //}

    //[InternalAjaxOnly]
    //[HttpPost("docs/create")]
    //public async Task<IActionResult> CreateDocuments([FromForm] DocumentRecordListRequest req, List<IFormFile> files)
    //{
    //    var result = await service.CreateDocuments(req, files);
    //    return Ok(result);
    //}

    //[InternalAjaxOnly]
    //[HttpDelete("{docId}/delete")]
    //public async Task<IActionResult> Delete([FromRoute] int docId)
    //{
    //    var result = await service.Delete(docId);
    //    return Ok(result);
    //}
    //#endregion
}
