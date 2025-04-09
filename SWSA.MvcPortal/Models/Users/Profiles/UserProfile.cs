using AutoMapper;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.Users.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, UserListVM>();
        CreateMap<User, UserVM>();
    }
}
