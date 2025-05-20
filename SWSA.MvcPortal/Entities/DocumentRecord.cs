using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;

public class DocumentRecord
{
    [Key]
    public int Id { get; set; }
    [SystemAuditLog("Document Date")]
    public DateTime DocumentDate { get; set; }   // Date Received (for Incoming) or Date Returned (for Outgoing)
    [SystemAuditLog("Document Flow Type")]
    public DocumentFlowType FlowType { get; set; } // Incoming or Outgoing  
    [SystemAuditLog("Document Type")]
    public DocumentType DocumentType { get; set; }
    [SystemAuditLog("Bag or Box Count")]
    public int BagOrBoxCount { get; set; }
    [SystemAuditLog("Remarks")]
    public string? Remark { get; set; } = null!;

    [ForeignKey(nameof(HandledByStaff))]
    public int HandledByStaffId { get; set; }

  
    // File attachment info
    public string? AttachmentFileName { get; set; } = null!;
    public string? AttachmentFilePath { get; set; } = null!;
    public User HandledByStaff { get; set; } = null!;
}
