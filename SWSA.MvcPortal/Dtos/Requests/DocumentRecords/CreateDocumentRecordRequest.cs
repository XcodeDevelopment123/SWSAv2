
using SWSA.MvcPortal.Commons.Enums;

namespace SWSA.MvcPortal.Dtos.Requests.DocumentRecords;

public class CreateDocumentRecordListRequest
{
    public List<CreateDocumentRecordRequest> Documents { get; set; } = new();
}

public class CreateDocumentRecordRequest
{
    //For local file storing
    public string CompanyName { get; set; }
    public string DepartmentName { get; set; }
    public int CompanyId { get; set; }
    //End
    public DateTime DocumentDate { get; set; }  // Date Received (for Incoming) or Date Returned (for Outgoing)
    public DocumentFlowType FlowType { get; set; } // Incoming or Outgoing  
    public DocumentType DocumentType { get; set; } 
    public int BagOrBoxCount { get; set; }
    public string? Remark { get; set; } = null!;
    public string HandledByStaffId { get; set; } = null!;
    public int CompanyDepartmentId { get; set; }

    public string? AttachmentFileName { get; set; } = null!;
    public string? AttachmentFilePath { get; set; } = null!;
}
