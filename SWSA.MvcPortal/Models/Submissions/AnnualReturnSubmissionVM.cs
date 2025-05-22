using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Submissions;

public class AnnualReturnSubmissionVM : Profile
{
    public AnnualReturnSubmissionVM()
    {
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmission>();
        CreateMap<AnnualReturnSubmission, AnnualReturnSubmissionVM>();

    }
}
