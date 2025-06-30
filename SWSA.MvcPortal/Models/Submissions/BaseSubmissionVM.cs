using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.DocumentRecords;

namespace SWSA.MvcPortal.Models.Submissions;

public abstract class BaseSubmissionVM
{
    public int SubmissionId { get; set; }
    public int WorkAssignmentId { get; set; }
    public string? Remarks { get; set; }
    public List<DocumentRecordVM> Documents { get; set; }
    public int ForYear { get; set; }
    public abstract string DisplayYearLabel { get; } //Let submission VMs override the display label depend on ForYear 
    public CompanySimpleInfoVM CompanySimpleInfo { get; set; } = new CompanySimpleInfoVM();

    public int GetCompanyId()
    {
        return CompanySimpleInfo.CompanyId;
    }

}
