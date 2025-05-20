
namespace SWSA.MvcPortal.Entities;

public class UserCompanyDepartment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public string Department{ get; set; } //Use constant for department name
    public virtual User User { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
}

