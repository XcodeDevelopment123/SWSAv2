using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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


public static class MonthOfYearExtensions
{
    public static string GetMonthLabel(this IEnumerable<MonthOfYear> months)
    {
        if (months == null)
            return "-";

        var sorted = months.Where(m => m != 0).Distinct().OrderBy(m => m).Select(c => c.GetIntValue()).ToList();
        if (sorted.Count == 0)
            return "-";

        var set = new HashSet<int>(sorted);

        if (set.SetEquals(Enumerable.Range(1, 12)))
            return "Full Year";
        if (set.SetEquals(Enumerable.Range(1, 6)))
            return "First Half";
        if (set.SetEquals(Enumerable.Range(7, 6)))
            return "Second Half";

        var ranges = GetMonthRanges(sorted);
        return string.Join(", ", ranges.Select(r => FormatMonthRange(r.start, r.end)));
    }

    private static List<(int start, int end)> GetMonthRanges(List<int> sortedMonths)
    {
        var result = new List<(int start, int end)>();
        int start = sortedMonths[0], end = start;

        for (int i = 1; i < sortedMonths.Count; i++)
        {
            if (sortedMonths[i] == end + 1)
                end = sortedMonths[i];
            else
            {
                result.Add((start, end));
                start = end = sortedMonths[i];
            }
        }
        result.Add((start, end));
        return result;
    }

    private static string FormatMonthRange(int start, int end)
    {
        string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
        if (start == end)
            return monthNames[start - 1];
        return $"{monthNames[start - 1]} ~ {monthNames[end - 1]}";
    }
}