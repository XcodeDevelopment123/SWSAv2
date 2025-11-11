using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class EnterpriseClient
{
    public int Id { get; set; }

    public virtual BaseCompany IdNavigation { get; set; } = null!;
}
