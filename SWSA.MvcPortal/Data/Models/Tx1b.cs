using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class Tx1b
{
    public int Id { get; set; }

    public string? CompanyName { get; set; }

    public string? YearEnd { get; set; }

    public string? Irbpenalties { get; set; }

    public string? AppealDate { get; set; }

    public string? PaymentDate { get; set; }

    public string? NoteRemark { get; set; }

    public string? AccWkDone { get; set; }

    public string? FormEsubmitDare { get; set; }

    public string? FormCsubmitDate { get; set; }

    public string? InvoiceDate { get; set; }

    public string? Amount { get; set; }

    public string? ClientSentCopy { get; set; }
}
