using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks.Profiles;

public class CompanyWorkProgressProfile:Profile
{
    public CompanyWorkProgressProfile()
    {
        CreateMap<WorkProgress, WorkProgress>();
    }
}
