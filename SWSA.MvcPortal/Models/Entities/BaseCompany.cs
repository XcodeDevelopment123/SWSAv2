using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class BaseCompany
{
    public int Id { get; set; }

    public string? FileNo { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public int CompanyType { get; set; }

    public DateTime? IncorporationDate { get; set; }

    public string? EmployerNumber { get; set; }

    public int ActivitySize { get; set; }

    public virtual ICollection<Aexbcklog> Aexbcklogs { get; set; } = new List<Aexbcklog>();

    public virtual ICollection<Aextemplate> Aextemplates { get; set; } = new List<Aextemplate>();

    public virtual ICollection<AuditBacklogSchedule> AuditBacklogSchedules { get; set; } = new List<AuditBacklogSchedule>();

    public virtual ICollection<AuditTemplate> AuditTemplates { get; set; } = new List<AuditTemplate>();

    public virtual ICollection<CompanyMsicCode> CompanyMsicCodes { get; set; } = new List<CompanyMsicCode>();

    public virtual ICollection<CompanyOwner> CompanyOwners { get; set; } = new List<CompanyOwner>();

    public virtual EnterpriseClient? EnterpriseClient { get; set; }

    public virtual Client IdNavigation { get; set; } = null!;

    public virtual Llpclient? Llpclient { get; set; }

    public virtual SdnBhdClient? SdnBhdClient { get; set; }

    public virtual ICollection<SecDeptTaskTemplate> SecDeptTaskTemplates { get; set; } = new List<SecDeptTaskTemplate>();

    public virtual ICollection<SecStrikeOffTemplate> SecStrikeOffTemplates { get; set; } = new List<SecStrikeOffTemplate>();

    public virtual ICollection<TaxStrikeOffTemplate> TaxStrikeOffTemplates { get; set; } = new List<TaxStrikeOffTemplate>();
}
