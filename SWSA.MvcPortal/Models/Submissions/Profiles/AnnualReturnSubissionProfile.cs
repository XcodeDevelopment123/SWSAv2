using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Submissions.Profiles;

public class AnnualReturnSubissionProfile : Profile
{
    public AnnualReturnSubissionProfile()
    {
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmission>();
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmissionVM>()
            .ForMember(dest => dest.SubmissionId, opt => opt.MapFrom(src => src.Id));
    }
}