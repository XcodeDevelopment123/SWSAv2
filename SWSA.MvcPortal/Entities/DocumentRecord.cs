using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities;
[Module("CompanyProfile")]
public class DocumentRecord
{
    [Key]
    public int Id { get; set; }
    public string Department { get; set; }

    [SystemAuditLog("Document Date")]
    public DateTime DocumentDate { get; set; } //For Flow out = Date Send to Dept, Flow In = Date Received
    [SystemAuditLog("Document Flow Type")]
    public DocumentFlowType DocumentFlow { get; set; } // Incoming or Outgoing  
    [SystemAuditLog("Document Type")]
    public DocumentType DocumentType { get; set; }
    [SystemAuditLog("Bag or Box Count")]
    public int BagOrBoxCount { get; set; }
    [SystemAuditLog("Upload Letter")]
    public string UploadLetter { get; set; }

    [SystemAuditLog("Remarks")]
    public string? Remark { get; set; } = null!;

    [ForeignKey(nameof(HandledByStaff))]
    public int HandledByStaffId { get; set; }
    public User HandledByStaff { get; set; } = null!;

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
