using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Backlogs;

public class AuditBacklogSchedule
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int QuarterToDoAudit { get; set; }
    public int YearToDo { get; set; }
    public string? ReasonForBacklog { get; set; }

    [ForeignKey(nameof(ClientId))]
    public BaseCompany Client { get; set; }
}
