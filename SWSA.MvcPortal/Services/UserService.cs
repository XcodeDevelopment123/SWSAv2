using AutoMapper;
using SWSA.MvcPortal.Repositories.Interfaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Services;

public class UserService(
IUserRepository userRepo,
IMapper mapper
) : IUserService
{

}
