using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class SystemNotificationLog
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Recipient { get; set; } = null!;

    public int Channel { get; set; }

    public bool IsSuccess { get; set; }

    public string ResultMessage { get; set; } = null!;
}
