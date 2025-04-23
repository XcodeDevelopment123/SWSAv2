using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SWSA.MvcPortal.Commons.Filters;

namespace SWSA.MvcPortal.Entities;

public class CompanyDepartment
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    [SystemAuditLog("Department Active Status")]
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;
    public Department Department { get; set; } = null!;

    public ICollection<CompanyStaff> CompanyStaffs { get; set; } = new List<CompanyStaff>();

    public CompanyDepartment(int departmentId)
    {
        DepartmentId = departmentId;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    public void SetActive()
    {
        IsActive = true;
    }

}
