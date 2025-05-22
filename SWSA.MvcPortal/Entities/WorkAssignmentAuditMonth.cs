using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class WorkAssignmentAuditMonth
{
    [Key]
    public int Id { get; set; }
    public MonthOfYear Month { get; set; }
    public int CompanyWorkAssignmentId { get; set; }
    public CompanyWorkAssignment CompanyWorkAssignment { get; set; } = null!;
}
