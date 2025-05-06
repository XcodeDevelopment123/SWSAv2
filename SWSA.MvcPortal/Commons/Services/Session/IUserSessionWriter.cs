using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Commons.Services.Session;

public interface IUserSessionWriter
{
    void Write(User user);
}
