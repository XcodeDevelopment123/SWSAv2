using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Entities.Models;

public partial class Group
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
