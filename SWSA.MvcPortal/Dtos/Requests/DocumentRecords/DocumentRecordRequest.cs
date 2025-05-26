using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.DocumentRecords;

public class DocumentRecordListRequest
{
    public List<DocumentRecordRequest> Documents { get; set; } = new();
}

public class DocumentRecordRequest
{
    public DateTime DocumentDate { get; set; }  // Date Received (for Incoming) or Date Returned (for Outgoing)
    public DocumentFlowType FlowType { get; set; } // Incoming or Outgoing  
    public DocumentType DocumentType { get; set; } 
    public int BagOrBoxCount { get; set; }
    public string? Remark { get; set; } = null!;

    public string? AttachmentFileName { get; set; } = null!;
    public string? AttachmentFilePath { get; set; } = null!;
}
