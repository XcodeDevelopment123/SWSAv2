using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class AuditBacklogSchedule
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int QuarterToDoAudit { get; set; }

    public int YearToDo { get; set; }

    public string? ReasonForBacklog { get; set; }

    public virtual BaseCompany Client { get; set; } = null!;
}
