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
        CreateMap<CreateCompanyWorkAssignmentRequest, CompanyWorkAssignment>();

        CreateMap<CompanyWorkAssignment, CompanyWorkListVM>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom((src, dest) => src.Company.Name))
            .ForMember(dest => dest.CompanyRegistrationNumber, opt => opt.MapFrom((src, dest) => src.Company.RegistrationNumber))
            .ForMember(dest => dest.ActivitySize, opt => opt.MapFrom(src => src.CompanyActivityLevel))
            .ForMember(dest => dest.YearEndToDo, opt => opt.MapFrom(src => src.IsYearEndTask))
            .ForMember(dest => dest.AuditMonthToDo, opt => opt.MapFrom(src => src.AuditPlannedMonths.Select(c => c.Month)))
            .ForMember(dest => dest.AccMonthToDo, opt => opt.MapFrom(src => src.AccountPlannedMonths.Select(c => c.Month)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Progress?.Status ?? WorkProgressStatus.Unknown))
            .AfterMap((src, dest) => dest.GenerateMonthLabel());

        CreateMap<CompanyWorkAssignment, CompanyWorkVM>()
             .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.ActivitySize, opt => opt.MapFrom(src => src.CompanyActivityLevel))
             .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
             .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.Progress))
             .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src => src.AssignedUsers));

        CreateMap<WorkAssignmentUserMapping, CompanyWorkUserVM>()
                 .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.User.StaffId))
                 .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.User.FullName))
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
                 .ForMember(dest => dest.IsAssignedToAccount, opt => opt.MapFrom(src => src.Department == DepartmentType.Account))
                 .ForMember(dest => dest.IsAssignedToAudit, opt => opt.MapFrom(src => src.Department == DepartmentType.Audit));

        CreateMap<WorkAssignmentAccountMonth, CompanyWorkMonthVM>();
        CreateMap<WorkAssignmentAuditMonth, CompanyWorkMonthVM>();

    }
}
