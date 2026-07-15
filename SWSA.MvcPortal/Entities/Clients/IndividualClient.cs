using SWSA.MvcPortal.Commons.Attributes;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Entities.Clients;

public class IndividualClient : BaseClient
{
    [Key]
    public int Id { get; set; }

    [SystemAuditLog("IC or Passport Number")]
    public string ICOrPassportNumber { get; set; } = null!;

    [SystemAuditLog("Professions")]
    public string Profession { get; set; }

    public void UpdateClientInfo(string name, string? tin, MonthOfYear? yearEnd, string icOrPass, string profession)
    {
        Name = name;
        TaxIdentificationNumber = tin;
        YearEndMonth = yearEnd;
        ICOrPassportNumber = icOrPass;
        Profession = profession;
    }

    public void UpdateAdminInfo(string? group, string? referral)
    {
        Group = group;
        Referral = referral;
    }
}
