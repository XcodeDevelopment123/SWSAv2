using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class S14b
{
    public int Id { get; set; }

    public string? FileNo { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyNo { get; set; }

    public string? IncorpDate { get; set; }

    public string? YearEnd { get; set; }

    public string? CompanyStatus { get; set; }

    public string? YrMthdueDate { get; set; }

    public string? CirculationAfsduedate { get; set; }

    public string? MbrsreceivedDate { get; set; }

    public string? OntimeLate { get; set; }

    public string? ReasonForLate { get; set; }

    public string? JobCompleted { get; set; }
}
