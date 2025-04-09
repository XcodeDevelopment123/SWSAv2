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
}
