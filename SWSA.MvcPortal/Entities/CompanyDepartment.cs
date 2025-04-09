using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class CompanyDepartment
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
    public bool IsActive { get; set; } = false;

    public Company Company { get; set; } = null!;
    public Department Department { get; set; } = null!;

    public ICollection<CompanyCommunicationContact> CommunicationContacts { get; set; } = new List<CompanyCommunicationContact>();

    public CompanyDepartment(int departmentId)
    {
        DepartmentId = departmentId;
    }

}
