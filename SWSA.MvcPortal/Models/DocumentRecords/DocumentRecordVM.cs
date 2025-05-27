using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Models.DocumentRecords;

public class DocumentRecordVM
{
    public int DocumentId { get; set; }
    public DateTime DocumentDate { get; set; }
    public DocumentFlowType FlowType { get; set; }
    public DocumentType DocumentType { get; set; }
    public string? AttachmentFileName { get; set; } = null!;
    public string HandledByStaffId { get; set; } = null!;
    public string StaffName { get; set; } = null!;
    public int BagOrBoxCount { get; set; }
    public WorkType WorkType { get; set; }
    public int WorkAssignmentId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string? Remark { get; set; } = null!;

    //For resource viewing or downloading
    public string AttachmentPath { get; set; } = null!;
    public string DownloadLink { get; set; } = null!;
    public string ViewLink { get; set; } = null!;

}
