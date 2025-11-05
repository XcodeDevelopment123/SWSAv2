using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class SecDeptTaskTemplate
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime? ArdueDate { get; set; }

    public DateTime? ArsubmitDate { get; set; }

    public DateTime? ArsendToClientDate { get; set; }

    public DateTime? ArreturnByClientDate { get; set; }

    public DateTime? AdsubmitDate { get; set; }

    public DateTime? AdsendToClientDate { get; set; }

    public DateTime? AdreturnByClientDate { get; set; }

    public string? Remarks { get; set; }

    public virtual BaseCompany Client { get; set; } = null!;
}
