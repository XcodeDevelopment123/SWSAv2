using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string? Group { get; set; }

    public string? Referral { get; set; }

    public string Name { get; set; } = null!;

    public int? YearEndMonth { get; set; }

    public string? TaxIdentificationNumber { get; set; }

    public int ClientType { get; set; }

    public bool IsActive { get; set; }

    public virtual BaseCompany? BaseCompany { get; set; }

    public virtual ICollection<CommunicationContact> CommunicationContacts { get; set; } = new List<CommunicationContact>();

    public virtual ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();

    public virtual IndividualClient? IndividualClient { get; set; }

    public virtual ICollection<OfficialContact> OfficialContacts { get; set; } = new List<OfficialContact>();
}
