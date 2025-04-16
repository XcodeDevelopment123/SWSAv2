using SWSA.MvcPortal.Commons.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class DocumentRecord
{
    [Key]
    public int Id { get; set; }
    public DateTime DocumentDate { get; set; }   // Date Received (for Incoming) or Date Returned (for Outgoing)
    public DocumentFlowType FlowType { get; set; } // Incoming or Outgoing  
    public DocumentType DocumentType { get; set; }
    public int BagOrBoxCount { get; set; }
    public string? Remark { get; set; } = null!;

    [ForeignKey(nameof(HandledByStaff))]
    public int HandledByStaffId { get; set; }

    [ForeignKey(nameof(Department))]
    public int CompanyDepartmentId { get; set; }
  
    // File attachment info
    public string? AttachmentFileName { get; set; } = null!;
    public string? AttachmentFilePath { get; set; } = null!;
    public CompanyDepartment? Department { get; set; } = null!;
    public User HandledByStaff { get; set; } = null!;
}
