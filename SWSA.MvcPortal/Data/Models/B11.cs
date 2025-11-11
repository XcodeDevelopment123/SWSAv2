using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class B11
{
    public int Id { get; set; }

    public string? Grouping { get; set; }

    public string? File { get; set; }

    public string? Company { get; set; }

    public string? CompanyNo { get; set; }

    public string? IncorporationDate { get; set; }

    public string? YearEnd { get; set; }

    public string? YmdueDate { get; set; }

    public string? CirculationAfsdueDate { get; set; }

    public string? ReminderDate { get; set; }

    public string? EmailSend { get; set; }

    public string? DateSent { get; set; }
}
