using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Companies;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Companies.Profiles;

public class CompanyCommunicationContactProfile : Profile
{
    public CompanyCommunicationContactProfile()
    {
        CreateMap<CreateCompanyCommunicationContactRequest, CompanyCommunicationContact>();
    }
}
