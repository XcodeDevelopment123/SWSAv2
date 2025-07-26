using SWSA.MvcPortal.Models.Companies;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.DocumentRecords;

public class DocumentRecordCreatePageVM
{
    public CompanySimpleInfoVM Company { get; set; }
    public List<UserSelectionVM> StaffSelections { get; set; } = [];


    public DocumentRecordCreatePageVM(
            CompanySimpleInfoVM company,
            List<UserSelectionVM> staffSelections
        )
    {
        Company = company;
        StaffSelections = staffSelections;
    }
}
