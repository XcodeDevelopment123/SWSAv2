using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class TaxStrikeOffTemplate
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public decimal? IrbpenaltiesAmount { get; set; }

    public DateTime? PenaltiesAppealDate { get; set; }

    public DateTime? PenaltiesPaymentDate { get; set; }

    public string? Remarks { get; set; }

    public bool IsAccountWorkComplete { get; set; }

    public DateTime? FormEsubmitDate { get; set; }

    public DateTime? FormCsubmitDate { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public decimal? InvoiceAmount { get; set; }

    public bool IsClientCopySent { get; set; }

    public virtual BaseCompany Client { get; set; } = null!;
}
