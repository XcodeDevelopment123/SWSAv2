using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Entities.Models;

public partial class At22
{
    public int Id { get; set; }

    public string? Grouping { get; set; }

    public string? CompanyName { get; set; }

    public string? QuarterToDoAudit { get; set; }

    public string? Activity { get; set; }

    public string? YearEnd { get; set; }

    public string? YearToDo { get; set; }

    public string? MoveToActiveAexSch { get; set; }

    public string? DateDocIn { get; set; }

    public string? AcctngWk { get; set; }

    public string? ReasonWhyBacklog { get; set; }
}
