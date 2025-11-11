using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class Bp33
{
    public int Id { get; set; }

    public string? Item { get; set; }

    public string? Grouping { get; set; }

    public string? CompanyName { get; set; }

    public string? DraftTaxCompleted { get; set; }

    public string? ReviewTax { get; set; }

    public string? FinalTax { get; set; }

    public string? TaxComFinalSignByClient { get; set; }

    public string? AmountofTaxPay { get; set; }

    public string? EfileDraft { get; set; }

    public string? EfileFinal { get; set; }

    public string? TaxReferennceNo { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? TypeofForm { get; set; }

    public string? Spc { get; set; }

    public string? InvoicesNo { get; set; }

    public string? DocDespatchDate { get; set; }
}
