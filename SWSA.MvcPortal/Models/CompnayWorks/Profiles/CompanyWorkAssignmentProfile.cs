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
        CreateMap<CreateCompanyWorkAssignmentRequest, CompanyWorkAssignment>()
            .ForMember(dest => dest.AssignedStaffId, opt => opt.Ignore());

        CreateMap<CompanyWorkAssignment, CompanyWorkListVM>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom((src, dest) => src.CompanyId))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom((src, dest) => src.Company.Name))
            .ForMember(dest => dest.StaffName, opt => opt.MapFrom((src, dest) => src.AssignedStaff?.ContactName ?? AppSettings.NotAvailable))
            .ForMember(dest => dest.StaffId, opt => opt.MapFrom((src, dest) => src.AssignedStaffId))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom((src, dest) => src.AssignedStaff?.CompanyDepartment?.Id ?? 0))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom((src, dest) => src.AssignedStaff?.CompanyDepartment?.Department?.Name ?? AppSettings.NotAvailable))
            .ForMember(dest => dest.ActivitySize, opt => opt.MapFrom(src => src.CompanyActivityLevel))
            .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Progress?.Status ?? WorkProgressStatus.Unknown))
           ;

    }
}
