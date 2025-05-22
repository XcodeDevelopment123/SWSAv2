using AutoMapper;
using SWSA.MvcPortal.Dtos.Requests.Users;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Users.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserListVM>();
        CreateMap<User, UserEditVM>();
        CreateMap<User, UserVM>();
        CreateMap<User, UserOverviewVM>()
           .ForMember(dest => dest.AssignedCompanies, opt => opt.MapFrom(src =>
           src.CompanyDepartments
           .Select(x => x.Company)
           .GroupBy(c => c.Id)
           .Select(g => g.First()))
           );

        CreateMap<User, UserSelectionVM>()
           .ForMember(dest => dest.CompanyDepartments, opt => opt.MapFrom(src =>
               src.CompanyDepartments
                  .GroupBy(cd => cd.CompanyId)
                  .ToDictionary(
                      g => g.Key,
                      g => g.Select(cd => cd.Department).Distinct().ToList()
                  )
           ));

        CreateMap<CreateUserRequest, User>();
    }
}
