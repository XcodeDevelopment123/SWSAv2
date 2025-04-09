
namespace SWSA.MvcPortal.Services.Interfaces;

public interface IUserService
{
    Task<bool> SetUserSession(string staffId);
}
