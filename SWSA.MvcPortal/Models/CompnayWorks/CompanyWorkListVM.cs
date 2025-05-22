using System.Globalization;
using Newtonsoft.Json;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Extensions;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkListVM
{
    public int TaskId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string CompanyRegistrationNumber { get; set; } = null!;
    public WorkType WorkType { get; set; }
    public ServiceScope ServiceScope { get; set; }
    public CompanyActivityLevel ActivitySize { get; set; }
    public WorkProgressStatus Status { get; set; }
    public string? InternalNote { get; set; }
    public bool YearEndToDo { get; set; } = false;
    public string AccMonthToDoLabel { get; set; } = null!;
    public string AuditMonthToDoLabel { get; set; } = null!;
    [JsonIgnore]
    public List<MonthOfYear> AccMonthToDo { get; set; } = [];
    [JsonIgnore]
    public List<MonthOfYear> AuditMonthToDo { get; set; } = [];

    public void GenerateMonthLabel()
    {
        AccMonthToDoLabel = GetMonthLabel(AccMonthToDo);
        AuditMonthToDoLabel = GetMonthLabel(AuditMonthToDo);
    }

    private static string GetMonthLabel(ICollection<MonthOfYear> months)
    {
        if (months == null || months.Count == 0)
            return "-";

        var sorted = months.Distinct().OrderBy(m => m).Select(c => c.GetIntValue()).ToList();
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
            {
                end = sortedMonths[i];
            }
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
