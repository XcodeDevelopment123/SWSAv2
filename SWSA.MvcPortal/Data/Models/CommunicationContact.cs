using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class CommunicationContact
{
    public int Id { get; set; }

    public string ContactName { get; set; } = null!;

    public string WhatsApp { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Remark { get; set; }

    public int Position { get; set; }

    public int ClientId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Client Client { get; set; } = null!;
}
