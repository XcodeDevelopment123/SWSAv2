using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Models.CompnayWorks;

public class CompanyWorkCalendarVM
{
    public string TaskId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public string? BackgroundColor { get; set; }
    public string? BorderColor { get; set; }
    public string? TextColor { get; set; }
    public string? Url { get; set; }
    public bool AllDay { get; set; } = true;

    public static (string background, string border, string text) GetColor(WorkAssignment task)
    {
        var progress = task.Progress?.Status;
        int hue = progress switch
        {
            WorkProgressStatus.Pending => 30,
            WorkProgressStatus.InProgress => 210,
            WorkProgressStatus.Completed => 120,
            WorkProgressStatus.Backlog => 0,
            _ => 0
        };

        //      int daysLeft = (task.DueDate.Date - DateTime.Today).Days;
        int daysLeft = 1;
        int lightness = Math.Clamp(70 - daysLeft * 3, 35, 70);
        int hash = task.Id.GetHashCode();
        int saturation = 60 + (Math.Abs(hash) % 20);

        string hsl = $"hsl({hue}, {saturation}%, {lightness}%)";
        return (hsl, hsl, "#fff");
    }

    public static CompanyWorkCalendarVM FromTask(WorkAssignment task)
    {
        var (bg, border, text) = GetColor(task);

        return new CompanyWorkCalendarVM
        {
            TaskId = task.Id.ToString(),
            Title = $"{task.ServiceScope} - {task.Client.Name}",
            Start = DateTime.Today,
            AllDay = true,
            BackgroundColor = bg,
            BorderColor = border,
            TextColor = text,
            Url = $"/companies/works/{task.Id}/edit"
        };
    }
}

