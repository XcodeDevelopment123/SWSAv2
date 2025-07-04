using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;
[Module("WorkAssignment")]
public class DocumentRecord
{
    [Key]
    public int Id { get; set; }
    [SystemAuditLog("Document Date")]
    public DateTime DocumentDate { get; set; }   // Date Received (for Incoming) or Date Returned (for Outgoing)
    [SystemAuditLog("Document Flow Type")]
    public DocumentFlowType DocumentFlow { get; set; } // Incoming or Outgoing  
    [SystemAuditLog("Document Type")]
    public DocumentType DocumentType { get; set; }
    [SystemAuditLog("Bag or Box Count")]
    public int BagOrBoxCount { get; set; }
    [SystemAuditLog("Remarks")]
    public string? Remark { get; set; } = null!;

    [ForeignKey(nameof(HandledByStaff))]
    public int HandledByStaffId { get; set; }

    [ForeignKey(nameof(WorkAssignment))]
    public int WorkAssignmentId { get; set; }
    public virtual CompanyWorkAssignment WorkAssignment { get; set; } = null!;

    // File attachment info
    public string? AttachmentFileName { get; set; } = null!;
    public string? AttachmentFilePath { get; set; } = null!;
    public User HandledByStaff { get; set; } = null!;

    public string GetDownloadLink()
    {
        if (string.IsNullOrEmpty(AttachmentFilePath))
            return null!;

        if (AttachmentFilePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            return AttachmentFilePath;

        //file controller and download link
        var downloadPath = $"files/download?path={Uri.EscapeDataString(AttachmentFilePath)}";
        if (!string.IsNullOrEmpty(AttachmentFileName))
        {
            downloadPath += $"&fileOriName={Uri.EscapeDataString(AttachmentFileName)}";
        }
        return $"{downloadPath}";
    }

    public string GetViewLink()
    {
        if (string.IsNullOrEmpty(AttachmentFilePath))
            return null!;

        if (AttachmentFilePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            return AttachmentFilePath;

        //file controller and download link
        var viewPath = $"files/view?path={Uri.EscapeDataString(AttachmentFilePath)}";
        if (!string.IsNullOrEmpty(AttachmentFileName))
        {
            viewPath += $"&fileOriName={Uri.EscapeDataString(AttachmentFileName)}";
        }
        return $"{viewPath}";
    }
}
