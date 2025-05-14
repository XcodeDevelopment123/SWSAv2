using AutoMapper;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Dtos.Requests.CompanyWorks;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkAssignmentProfile : Profile
{
    public CompanyWorkAssignmentProfile()
    {
        CreateMap<CompanyWorkAssignment, CompanyWorkAssignment>();
        CreateMap<CreateCompanyWorkAssignmentRequest, CompanyWorkAssignment>()
        .ForMember(dest => dest.AssignedUserId, opt => opt.Ignore());

        CreateMap<CompanyWorkAssignment, CompanyWorkListVM>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom((src, dest) => src.Company.Name))
            .ForMember(dest => dest.StaffName, opt => opt.MapFrom((src, dest) => src.AssignedUser?.FullName ?? AppSettings.NotAvailable))
            .ForMember(dest => dest.StaffId, opt => opt.MapFrom((src, dest) => src.AssignedUser?.Id))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom((src, dest) => src.CompanyDepartment?.Id ?? 0))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom((src, dest) => src.CompanyDepartment?.Department?.Name ?? AppSettings.NotAvailable))
            .ForMember(dest => dest.ActivitySize, opt => opt.MapFrom(src => src.CompanyActivityLevel))
            .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Progress?.Status ?? WorkProgressStatus.Unknown));

        CreateMap<CompanyWorkAssignment, CompanyWorkVM>()
             .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.ActivitySize, opt => opt.MapFrom(src => src.CompanyActivityLevel))
             .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.AssignedUser))
             .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
             .ForMember(dest => dest.CompanyDepartment, opt => opt.MapFrom((src, dest) => src.CompanyDepartment ?? null!))
             .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.Progress))
             ;
    }
}
