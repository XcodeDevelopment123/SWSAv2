using SWSA.MvcPortal.Commons.Constants;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        if (enumValue == null)
            return AppSettings.NotAvailable;

        var attribute = enumValue.GetType()
            .GetField(enumValue.ToString())?
            .GetCustomAttribute<DisplayAttribute>();

        return attribute?.Name ?? enumValue.ToString().SplitCamelCase();
    }
    public static int GetIntValue(this Enum enumValue)
    {
        if (enumValue == null)
            return 0;

        return Convert.ToInt32(enumValue);
    }

    public static string GetDisplayNameAndNumber(this Enum enumValue)
    {
        if (enumValue == null)
            return AppSettings.NotAvailable;

        return $"{GetIntValue(enumValue)} ({GetDisplayName(enumValue)})";
    }



}
