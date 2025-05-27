using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.DocumentRecords;

namespace SWSA.MvcPortal.Models.Submissions;

public class BaseSubmissionVM
{
    public int SubmissionId { get; set; }
    public int WorkAssignmentId { get; set; }
    public List<DocumentRecordVM> Documents { get; set; }
    public CompanySimpleInfoVM CompanySimpleInfo { get; set; } = new CompanySimpleInfoVM();

    public int GetCompanyId()
    {
        return CompanySimpleInfo.CompanyId;
    }
}
