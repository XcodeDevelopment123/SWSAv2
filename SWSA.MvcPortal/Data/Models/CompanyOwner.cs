using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class CompanyOwner
{
    public int Id { get; set; }

    public int ClientCompanyId { get; set; }

    public string NamePerIc { get; set; } = null!;

    public string IcorPassportNumber { get; set; } = null!;

    public int Position { get; set; }

    public string TaxReferenceNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool RequiresFormBesubmission { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual BaseCompany ClientCompany { get; set; } = null!;
}
