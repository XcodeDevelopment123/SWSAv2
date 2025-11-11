using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class OfficialContact
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string OfficeTel { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Remark { get; set; }

    public DateTime CreatedAt { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
