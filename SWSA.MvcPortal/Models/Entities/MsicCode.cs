using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class MsicCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CategoryReferences { get; set; } = null!;

    public virtual ICollection<CompanyMsicCode> CompanyMsicCodes { get; set; } = new List<CompanyMsicCode>();
}
