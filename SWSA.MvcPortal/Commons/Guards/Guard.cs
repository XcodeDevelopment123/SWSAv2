using SWSA.MvcPortal.Commons.Exceptions;

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

    //public static void AgainstCrossCompanyAccess(int targetCompanyId, IUserContext user)
    //{
    //    if (user.IsCompanyStaff && user.CompanyId != targetCompanyId)
    //    {
    //        throw new BusinessLogicException("You are not authorized to access data from another company.");
    //    }
    //}

}
