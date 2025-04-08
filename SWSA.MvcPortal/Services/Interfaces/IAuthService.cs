using SWSA.MvcPortal.Dtos.Responses;

namespace SWSA.MvcPortal.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResult> Login(string username, string password);
}
