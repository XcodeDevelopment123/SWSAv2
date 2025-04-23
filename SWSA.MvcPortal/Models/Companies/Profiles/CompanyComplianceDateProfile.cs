using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyComplianceDateProfile : Profile
{
    public CompanyComplianceDateProfile()
    {
        CreateMap <CompanyComplianceDate, CompanyComplianceDate>();
        CreateMap <CreateCompanyComplianceDate, CompanyComplianceDate>();
    }
}
