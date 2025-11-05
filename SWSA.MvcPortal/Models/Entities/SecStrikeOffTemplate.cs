using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class SecStrikeOffTemplate
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? CompleteDate { get; set; }

    public int? DoneByUserId { get; set; }

    public decimal? PenaltiesAmount { get; set; }

    public decimal? RevisedPenaltiesAmount { get; set; }

    public DateTime? SsmpenaltiesAppealDate { get; set; }

    public DateTime? SsmpenaltiesPaymentDate { get; set; }

    public DateTime? SsmdocSentDate { get; set; }

    public DateTime? SsmsubmissionDate { get; set; }

    public string? Remarks { get; set; }

    public virtual BaseCompany Client { get; set; } = null!;

    public virtual User? DoneByUser { get; set; }
}
