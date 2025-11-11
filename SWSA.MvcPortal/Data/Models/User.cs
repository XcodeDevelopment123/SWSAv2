using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string? StaffId { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsLocked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public string Department { get; set; } = null!;

    public int Role { get; set; }

    public DateTime JoinDate { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Aextemplate> Aextemplates { get; set; } = new List<Aextemplate>();

    public virtual ICollection<AuditTemplate> AuditTemplates { get; set; } = new List<AuditTemplate>();

    public virtual ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();

    public virtual ICollection<ScheduledJob> ScheduledJobs { get; set; } = new List<ScheduledJob>();

    public virtual ICollection<SecStrikeOffTemplate> SecStrikeOffTemplates { get; set; } = new List<SecStrikeOffTemplate>();

    public virtual ICollection<SystemAuditLog> SystemAuditLogs { get; set; } = new List<SystemAuditLog>();
}
