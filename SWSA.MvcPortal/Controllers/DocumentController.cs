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
    ICompanyDepartmentService companyDepartmentService,
    IUserService userService,
    ICompanyService companyService,
    IUploadFileService uploadFileService
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
        var dpts = cp.Departments.ToList();
        var staff = await userService.GetUserSelectionAsync();
        var vm = new DocumentRecordCreatePageVM(cp, dpts, staff);
        return View(vm);
    }

    [Route("docs/create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDocumentRecordListRequest req, List<IFormFile> files)
    {
        for (int i = 0; i < req.Documents.Count; i++)
        {
            var doc = req.Documents[i];

            if (i < files.Count && files[i] != null)
            {
                var flowType = doc.FlowType.ToString().ToLower();

                var safeCompanyName = SanitizeFolderName($"{doc.CompanyName}-{doc.CompanyId}");
                var safeDeptName = SanitizeFolderName($"{doc.DepartmentName}-{doc.CompanyDepartmentId}");
                var subFolder = Path.Combine("docs", safeCompanyName, safeDeptName, flowType);

                var result = await uploadFileService.UploadAsync(files[i], subFolder, UploadStorageType.Local);
                doc.AttachmentFilePath = result;
            }
            // TODO: Save to database
            await service.CreateDocument(doc);
        }

        return Json(true);
    }

    [Route("docs/{docId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int docId)
    {
        var result = await service.DeleteDocumentById(docId);
        if (result != null)
        {
            var filePath = result.AttachmentPath;
            if (!string.IsNullOrEmpty(filePath))
            {
                await uploadFileService.DeleteAsync(filePath);
            }
        }

        return Json(true);
    }

    private string SanitizeFolderName(string input)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
            input = input.Replace(c.ToString(), "");

        return input.Trim().Replace(" ", "_");
    }
}
