using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Models;

[Table("Referrals", Schema = "dbo")]
public partial class Referral
{
    public int Id { get; set; }

    public string ReferralName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
