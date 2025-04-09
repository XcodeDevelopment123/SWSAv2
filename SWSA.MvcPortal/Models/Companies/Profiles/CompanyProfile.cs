using AutoMapper;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyListVM>()
           .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
           .ForMember(dest => dest.CompanyType, opt => opt.MapFrom(src => src.CompanyType.Name ?? AppSettings.NotAvailable))
           .ForMember(dest => dest.CompanyDirectorName, opt => opt.MapFrom((src, dest) =>
           {
               var firstOwner = src.CompanyOwners.FirstOrDefault(c => c.Position == PositionType.Director);
               return firstOwner?.NamePerIC ?? AppSettings.NotAvailable;
           }))
           .ForMember(dest => dest.ContactsCount, opt => opt.MapFrom((src, dest) =>
           {
               return src.CommunicationContacts.Count + src.OfficialContacts.Count;
           }))
           .ForMember(dest => dest.DepartmentsCount, opt => opt.MapFrom(src => src.Departments.Count))
           .ForMember(dest => dest.MsicCodesCount, opt => opt.MapFrom(src => src.MsicCodes.Count));

        CreateMap<CreateCompanyRequest, Company>()
            .ForMember(dest => dest.MsicCodes, opt => opt.MapFrom(src => src.MsicCodeIds.Select(id => new CompanyMsicCode(id)).ToList()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
            ;
    }
}
