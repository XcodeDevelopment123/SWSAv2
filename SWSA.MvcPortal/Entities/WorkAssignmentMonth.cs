using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

[Module("WorkAssignment")]
public class WorkAssignmentMonth
{
    [Key]
    public int Id { get; set; }
    public MonthOfYear Month { get; set; }
    public int CompanyWorkAssignmentId { get; set; }
    public CompanyWorkAssignment CompanyWorkAssignment { get; set; } = null!;
}
