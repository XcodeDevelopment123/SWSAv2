using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class CompanyMsicCode
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int MsicCodeId { get; set; }

    public virtual BaseCompany Company { get; set; } = null!;

    public virtual MsicCode MsicCode { get; set; } = null!;
}
