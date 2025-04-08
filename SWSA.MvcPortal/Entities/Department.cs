using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class Department
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!; // e.g. Audit, Tax, Secretary -- use constant

    public Department(string name)
    {
        Name = name;
    }
}
