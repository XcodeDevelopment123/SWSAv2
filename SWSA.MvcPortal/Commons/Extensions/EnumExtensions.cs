using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var attribute = enumValue.GetType()
            .GetField(enumValue.ToString())?
            .GetCustomAttribute<DisplayAttribute>();

        return attribute?.Name ?? SplitCamelCase(enumValue.ToString());
    }
    public static int GetIntValue(this Enum enumValue)
    {
        return Convert.ToInt32(enumValue);
    }

    private static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
    }
}
