namespace SWSA.MvcPortal.Commons.Extensions;

public static class DateTimeExtensions
{
    public static string ToDisplayString(this DateTime? dateTime, string format = "yyyy-MM-dd")
    {
        return dateTime.HasValue
            ? dateTime.Value.ToString(format)
            : string.Empty;
    }

    public static string ToInputDateValue(this DateTime? dateTime, string format = "yyyy-MM-dd")
    {
        return dateTime.HasValue
            ? dateTime.Value.ToString(format)
            : string.Empty;
    }
}
