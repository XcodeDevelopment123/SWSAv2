using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Entities;

public class WorkAssignmentAccountMonth
{
    [Key]
    public int Id { get; set; }

    public MonthOfYear Month { get; set; }

    public int CompanyWorkAssignmentId { get; set; }
    public CompanyWorkAssignment CompanyWorkAssignment { get; set; } = null!;
}
