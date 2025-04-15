using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Controllers;

[Route("companies")]
public class CompanyStaffController(
    ICompanyStaffService service,   
    ICompanyService companyService
    ) : BaseController
{
    [Route("{companyId}/staff-list")]
    public async Task<IActionResult> StaffList([FromRoute] int companyId)
    {
        var data = await service.GetStaffsByCompanyId(companyId);
        return View(data);
    }

    [Route("staffs/{staffId}/edit")]
    public async Task<IActionResult> Edit([FromRoute] string staffId)
    {
        var data = await service.GetStaffByStaffId(staffId);
        if (data.Company != null)
            data.Company = await companyService.GetCompanyByIdAsync(data.Company.Id);
        return View(data);
    }

    [Route("staffs/create")]
    [HttpPost]
    public async Task<IActionResult> CreateStaffContact(CreateCompanyStaffRequest req)
    {
        var result = await service.CreateContact(req);
        return Json(true);
    }

    [Route("staffs/edit")]
    [HttpPost]
    public async Task<IActionResult> EditStaffContact(EditCompanyStaffInfoRequest req)
    {
        var result = await service.EditContact(req);
        return Json(true);
    }

    [Route("staffs/edit-login-profile")]
    [HttpPost]
    public async Task<IActionResult> UpdatStaffLogin(EditCompanyStaffLoginProfileRequest req)
    {
        var result = await service.EditLoginProfile(req);
        return Json(true);
    }

    [Route("staffs/{contactId}/delete")]
    [HttpDelete]
    public async Task<IActionResult> DeleteStaffContact([FromRoute] string contactId)
    {
        var result = await service.DeleteContact(contactId);
        return Json(true);
    }
}
