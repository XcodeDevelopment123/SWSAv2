using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.DocumentRecords;

public class DocumentRecordListRequest
{
    public List<DocumentRecordRequest> Documents { get; set; } = new();
}

public class DocumentRecordRequest
{
    public int CompanyId { get; set; }
    public string Department { get; set; }
    public DateTime DocumentDate { get; set; }  // Date Received (for Incoming) or Date Returned (for Outgoing)
    public DocumentFlowType FlowType { get; set; } // Incoming or Outgoing  
    public DocumentType DocumentType { get; set; }
    public int BagOrBoxCount { get; set; }
    public string? UploadLetter { get; set; } = null!;
    public string? Remark { get; set; } = null!;
}
