using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class SdnBhdClient
{
    public int Id { get; set; }

    public virtual BaseCompany IdNavigation { get; set; } = null!;
}
