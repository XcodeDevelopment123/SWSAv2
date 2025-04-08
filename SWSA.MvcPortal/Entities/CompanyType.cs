using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities;

public class CompanyType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!; // e.g. Sdn Bhd, LLP, Enterprise - Use Constants

    public CompanyType(string name)
    {
        Name = name;
    }
}
