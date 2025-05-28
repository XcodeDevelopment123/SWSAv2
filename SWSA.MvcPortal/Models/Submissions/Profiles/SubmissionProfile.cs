using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Submissions.Profiles;

public class SubmissionProfile : Profile
{
    public SubmissionProfile()
    {
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmission>();
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmissionVM>()
            .ForMember(dest => dest.SubmissionId, opt => opt.MapFrom(src => src.Id));

        CreateMap<CompanyStrikeOffSubmissionVM, CompanyStrikeOffSubmissionVM>();
        CreateMap<AuditSubmissionVM, AuditSubmissionVM>();
        CreateMap<LLPSubmissionVM, LLPSubmissionVM>();
        CreateMap<AnnualReturnSubmissionVM, AnnualReturnSubmissionVM>();
    }
}
