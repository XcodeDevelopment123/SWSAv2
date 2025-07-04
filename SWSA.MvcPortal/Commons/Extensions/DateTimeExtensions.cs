using SWSA.MvcPortal.Commons.Constants;

namespace SWSA.MvcPortal.Commons.Extensions;

public static class DateTimeExtensions
{
    public static string ToFormattedString(this DateTime dateTime, DateTimeFormatType formatType = DateTimeFormatType.DateOnly)
    {
        var format = formatType switch
        {
            DateTimeFormatType.DateOnly => DateTimeFormats.DateOnly,
            DateTimeFormatType.DateTime24H => DateTimeFormats.DateTime24H,
            DateTimeFormatType.DateTime12H => DateTimeFormats.DateTime12H,
            DateTimeFormatType.TimeOnly => DateTimeFormats.TimeOnly,
            DateTimeFormatType.Iso8601 => DateTimeFormats.Iso8601,
            _ => DateTimeFormats.DateOnly
        };

        return dateTime.ToString(format);
    }

    public static string ToFormattedString(this DateTime? dateTime, DateTimeFormatType formatType = DateTimeFormatType.DateOnly)
    {
        if (!dateTime.HasValue) return AppSettings.NotAvailable;

        var format = formatType switch
        {
            DateTimeFormatType.DateOnly => DateTimeFormats.DateOnly,
            DateTimeFormatType.DateTime24H => DateTimeFormats.DateTime24H,
            DateTimeFormatType.DateTime12H => DateTimeFormats.DateTime12H,
            DateTimeFormatType.TimeOnly => DateTimeFormats.TimeOnly,
            DateTimeFormatType.Iso8601 => DateTimeFormats.Iso8601,
            _ => DateTimeFormats.DateOnly
        };

        return dateTime.Value.ToString(format);
    }

   
}

/// <summary>
/// Commonly used DateTime format types for consistent display formatting.
/// </summary>
public enum DateTimeFormatType
{
    /// <summary>
    /// Date only in ISO format. suitable for frontend date picker.
    /// Format: yyyy-MM-dd
    /// Example: 2025-04-15
    /// </summary>
    DateOnly,

    /// <summary>
    /// Date and time in 24-hour format.
    /// Format: yyyy-MM-dd HH:mm
    /// Example: 2025-04-15 18:30
    /// </summary>
    DateTime24H,

    /// <summary>
    /// Date and time in 12-hour format with AM/PM.
    /// Format: yyyy-MM-dd hh:mm tt
    /// Example: 2025-04-15 06:30 PM
    /// </summary>
    DateTime12H,

    /// <summary>
    /// Time only in 24-hour format.
    /// Format: HH:mm
    /// Example: 18:30
    /// </summary>
    TimeOnly,

    /// <summary>
    /// ISO 8601 format with T separator.
    /// Format: yyyy-MM-ddTHH:mm:ss
    /// Example: 2025-04-15T18:30:00
    /// </summary>
    Iso8601
}

public static class DateTimeFormats
{
    public const string DateOnly = "yyyy-MM-dd";
    public const string DateTime24H = "yyyy-MM-dd HH:mm";
    public const string DateTime12H = "yyyy-MM-dd hh:mm tt";
    public const string TimeOnly = "HH:mm";
    public const string Iso8601 = "yyyy-MM-ddTHH:mm:ss";
}