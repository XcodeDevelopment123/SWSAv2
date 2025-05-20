using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Users;

namespace SWSA.MvcPortal.Models.DocumentRecords;

public class DocumentRecordCreatePageVM
{
    public Company Company { get; set; }
    public List<UserSelectionVM> StaffSelections { get; set; } = [];


    public DocumentRecordCreatePageVM(
            Company company,
            List<UserSelectionVM> staffSelections
        )
    {
        Company = company;
        StaffSelections = staffSelections;
    }
}
