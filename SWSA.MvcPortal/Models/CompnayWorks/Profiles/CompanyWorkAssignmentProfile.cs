using AutoMapper;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkAssignmentProfile : Profile
{
    public CompanyWorkAssignmentProfile()
    {
        CreateMap<CompanyWorkAssignment, CompanyWorkAssignment>();
        CreateMap<CreateCompanyWorkAssignmentRequest, CompanyWorkAssignment>();

        CreateMap<CompanyWorkAssignment, CompanyWorkListVM>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom((src, dest) => src.Company.Name))
            .ForMember(dest => dest.CompanyRegistrationNumber, opt => opt.MapFrom((src, dest) => src.Company.RegistrationNumber))
            .ForMember(dest => dest.YearEndToDo, opt => opt.MapFrom(src => src.IsYearEndTask))
            .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Progress?.Status ?? WorkProgressStatus.Unknown));

        CreateMap<CompanyWorkAssignment, CompanyWorkVM>()
             .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id));


        CreateMap<WorkAssignmentUserMapping, CompanyWorkUserVM>()
                 .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.User.StaffId))
                 .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.User.FullName))
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role));

        CreateMap<WorkAssignmentMonth, CompanyWorkMonthVM>();

    }
}
