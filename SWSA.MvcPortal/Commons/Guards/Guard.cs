using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;

namespace SWSA.MvcPortal.Commons.Guards;

public static class Guard
{
    public static void AgainstNullData<T>(T data, string message = "Data not found.")
    {
        if (data == null)
        {
            throw new DataNotFoundException(message);
        }
    }

    public static void AgainstNotExist(bool exist, string message = "Data not found.")
    {
        if (!exist)
        {
            throw new DataNotFoundException(message);
        }
    }

    public static void AgainstNotSuperAdmin(IUserContext user)
    {
        if (!user.IsSuperAdmin)
        {
            throw new DataNotFoundException("Only Super admin can perform this action");
        }
    }

    //public static void AgainstUnauthorizedCompanyAccess(int targetCompanyId, string? targetDepartment, IUserContext user)
    //{
    //    if (user.Role == UserRole.SuperAdmin)
    //        return;


    //    if (!user.AllowedCompanyIds.Contains(targetCompanyId))
    //        throw new UnauthorizedAccessException("Access denied to the specified company.");

    //    if (string.IsNullOrEmpty(targetDepartment))
    //        return;
        
    //    if (!user.AllowedDepartments.TryGetValue(targetCompanyId, out var depts) || !depts.Contains(targetDepartment))
    //    {
    //        throw new UnauthorizedAccessException("Access denied to the specified department.");
    //    }
    //}

}
