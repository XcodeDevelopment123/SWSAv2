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
    public string MonthToDoLabel => MonthToDo.GetMonthLabel();
    [JsonIgnore]
    public List<MonthOfYear> MonthToDo { get; set; } = [];
}
