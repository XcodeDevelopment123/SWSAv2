using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;
using SWSA.MvcPortal.Models.DocumentRecords;
using SWSA.MvcPortal.Services.Interfaces;
using SWSA.MvcPortal.Services.Interfaces.Clients;
using SWSA.MvcPortal.Services.Interfaces.UserAccess;

namespace SWSA.MvcPortal.Controllers;


[Route("documents")]
public class DocumentController(
    IDocumentRecordService service,
    IClientService _clientService,
    IUserService userService
    ) : BaseController
{
    #region Page/View
    [Route("audit-dept")]
    public async Task<IActionResult> AuditDepartment()
    {
        var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Audit);
        //Only Sdn Bhd has audit document;
        var clients = await _clientService.GetClientSelectionVM([ClientType.SdnBhd]);
        var vm = new DocumentRecordAuditDeptPageVM(clients, documents);
        return View(vm);
    }

    [Route("account-dept")]
    public async Task<IActionResult> AccountDepartment()
    {
        var documents = await service.GetDocumentRecordsByDepartment(DepartmentType.Account);
        return View(documents);
    }

    [Route("doct-audA31A")]
    public async Task<IActionResult> A31ADocControl()
    {
        return View();
    }

    [Route("doct-audA31B")]
    public async Task<IActionResult> A31BDocControl()
    {
        return View();
    }

    [Route("doct-accA32A")]
    public async Task<IActionResult> A32AcorrespondanceRecord()
    {
        return View();
    }
    
    [Route("doct-accA32B")]
    public async Task<IActionResult> A32BForm()
    {
        return View();
    }

    [Route("doct-taxA33A")]
    public async Task<IActionResult> A33ATaxDeptcorrespondanceRecord()
    {
        return View();
    }

    [Route("doct-taxA33B")]
    public async Task<IActionResult> A33BSdnBhdTaxAudit()
    {
        return View();
    }
    #endregion



}
