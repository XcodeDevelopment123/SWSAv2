using SWSA.MvcPortal.Dtos.Requests.DocumentRecords;

namespace SWSA.MvcPortal.Dtos.Requests.CompanyStrikeOffSubmissions;

public class EditCompanyStrikeOffSubmissionRequest
{
    public int CompanyId { get; set; }
    public int SubmissionId { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? SSMSubmissionDate { get; set; }
    public DateTime? IRBSubmissionDate { get; set; }
    public DateTime? SSMStrikeOffDate { get; set; }
    public string? Remarks { get; set; }

    public List<DocumentRecordRequest> Documents { get; set; } = new List<DocumentRecordRequest>();
}
