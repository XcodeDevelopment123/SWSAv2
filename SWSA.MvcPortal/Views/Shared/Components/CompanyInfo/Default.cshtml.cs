using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Views.Shared.Components.CompanyInfo;

public class CompanyInfoViewComponent(IMapper _mapper) : ViewComponent
{
    public IViewComponentResult Invoke(object input)
    {
        var raw = input?.GetType().GetProperty("input")?.GetValue(input);

        CompanySimpleInfoVM? vm = raw switch
        {
            CompanySimpleInfoVM simple => simple,
            _ => null
        };

        return View(vm);
    }
}
