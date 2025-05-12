namespace SWSA.MvcPortal.Dtos.Requests.Users;

public class EditUserCompanyDepartment
{
    public string StaffId { get; set; }
    public List<int> DepartmentIds { get; set; } = new();
    public int CompanyId { get; set; }  
}
