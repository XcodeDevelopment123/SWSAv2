using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Contacts;

public class CompanyOwner
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Company))]
    public int ClientCompanyId { get; set; }
    [SystemAuditLog("Owner Name (per IC)")]
    public string NamePerIC { get; set; } = null!;
    [SystemAuditLog("IC or Passport Number")]
    public string ICOrPassportNumber { get; set; } = null!;
    [SystemAuditLog("Position")]
    public PositionType Position { get; set; } = PositionType.Director;
    [SystemAuditLog("Tax Reference Number")]
    public string TaxReferenceNumber { get; set; } = null!;
    [SystemAuditLog("Email Address")]
    public string Email { get; set; } = null!;
    [SystemAuditLog("Phone Number")]
    public string PhoneNumber { get; set; } = null!;
    [SystemAuditLog("Form BE submission")]
    public bool RequiresFormBESubmission { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual BaseCompany Company { get; set; } = null!;

    public void UpdateInfo(string name, string icOrPassport, PositionType position, string taxRef, string email, string phoneNumber, bool isRequireSubmitFormBE)
    {
        NamePerIC = name;
        ICOrPassportNumber = icOrPassport;
        Position = position;
        TaxReferenceNumber = taxRef;
        Email = email;
        PhoneNumber = phoneNumber;
        RequiresFormBESubmission = isRequireSubmitFormBE;
    }
}
