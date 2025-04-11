using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyController(
    ICompanyService service,
    ICompanyOwnerService companyOwnerService,
    ICompanyCommunicationContactService companyCommunicationContactService,
    ICompanyComplianceDateService companyComplianceDateService,
    ICompanyOfficialContactService companyOfficialContactService,
    IMsicCodeService msicCodeService,
    ICompanyTypeService companyTypeService,
    IDepartmentService departmentService
    ) : BaseController
{
    [Route("")]
    public async Task<IActionResult> List()
    {
        var data = await service.GetCompaniesAsync();
        return View(data);
    }

    [Route("master-list")]
    public IActionResult MasterList()
    {
        return View();
    }

    [Route("{companyId}/overview")]
    public async Task<IActionResult> Overview([FromRoute] int companyId)
    {
        var data = await service.GetCompanyByIdAsync(companyId);
        return View(data);
    }

    [Route("create")]
    public async Task<IActionResult> Create()
    {
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        var companyTypes = await companyTypeService.GetCompanyTypesAsync();
        var departments = await departmentService.GetDepartments();

        CompanyCreatePageVM vm = new(msicCodes, companyTypes, departments);
        return View(vm);
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyRequest req)
    {
        var result = await service.CreateCompany(req);
        return Json(result);
    }

    [Route("{companyId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int companyId)
    {
        var cp = await service.GetCompanyByIdAsync(companyId);
        var msicCodes = await msicCodeService.GetMsicCodeAsync();
        var companyTypes = await companyTypeService.GetCompanyTypesAsync();
        var departments = await departmentService.GetDepartments();

        CompanyEditPageVM vm = new(cp, msicCodes, companyTypes,departments);
        return View(vm);
    }

    [Route("edit")]
    [HttpPost]
    public async Task<IActionResult> Edit(EditCompanyRequest req)
    {
        var result = await service.UpdateCompanyInfo(req);
        return Json(result);
    }

    [Route("{companyId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int companyId)
    {
        var data = await service.DeleteCompanyByIdAsync(companyId);
        return View(data);
    }

    [Route("compliance-date/edit")]
    [HttpPost]
    public async Task<IActionResult> EditComplianceDate(EditCompanyComplianceDate req)
    {
        var result = await companyComplianceDateService.SaveComplianceDate(req);
        return Json(true);
    }

    [Route("owner/create")]
    [HttpPost]
    public async Task<IActionResult> CreateOwner(CreateCompanyOwnerRequest req)
    {
        var result = await companyOwnerService.CreateOwner(req);
        return Json(true);
    }

    [Route("owner/edit")]
    [HttpPost]
    public async Task<IActionResult> EditOwner(EditCompanyOwnerRequest req)
    {
        var result = await companyOwnerService.EditOwner(req);
        return Json(true);
    }

    [Route("owner/{ownerId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> DeleteOwner([FromRoute] int ownerId)
    {
        var result = await companyOwnerService.DeleteOwner(ownerId);
        return Json(true);
    }

    [Route("communication-contact/create")]
    [HttpPost]
    public async Task<IActionResult> CreateStaffContact(CreateCompanyCommunicationContactRequest req)
    {
        var result = await companyCommunicationContactService.CreateContact(req);
        return Json(true);
    }

    [Route("communication-contact/edit")]
    [HttpPost]
    public async Task<IActionResult> EditStaffContact(EditCompanyCommunicationContactRequest req)
    {
        var result = await companyCommunicationContactService.EditContact(req);
        return Json(true);
    }

    [Route("communication-contact/{contactId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> DeleteStaffContact([FromRoute] int contactId)
    {
        var result = await companyCommunicationContactService.DeleteContact(contactId);
        return Json(true);
    }

    [Route("official-contact/create")]
    [HttpPost]
    public async Task<IActionResult> CreateOfficeContact(CreateCompanyOfficialContactRequest req)
    {
        var result = await companyOfficialContactService.CreateContact(req);
        return Json(true);
    }

    [Route("official-contact/edit")]
    [HttpPost]
    public async Task<IActionResult> EditOfficeContact(EditCompanyOfficialContactRequest req)
    {
        var result = await companyOfficialContactService.EditContact(req);
        return Json(true);
    }

    [Route("official-contact/{contactId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> DeleteOfficeContact([FromRoute] int contactId)
    {
        var result = await companyOfficialContactService.DeleteContact(contactId);
        return Json(true);
    }

}
