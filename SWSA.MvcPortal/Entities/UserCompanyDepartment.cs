
using SWSA.MvcPortal.Dtos.Requests.Users;

namespace SWSA.MvcPortal.Entities;

public class UserCompanyDepartment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
}

