using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class IndividualClient
{
    public int Id { get; set; }

    public string IcorPassportNumber { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public virtual Client IdNavigation { get; set; } = null!;
}
