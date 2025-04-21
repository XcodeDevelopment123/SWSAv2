namespace SWSA.MvcPortal.Commons.Extensions;

public static class StringExtensions
{
    public static string SplitCamelCase(this string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
    }
}
