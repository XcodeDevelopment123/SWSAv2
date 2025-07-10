using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Companies;

namespace SWSA.MvcPortal.Models.DocumentRecords;

public class DocumentRecordAuditDeptPageVM
{
    public List<CompanySelectionVM> Companies { get; set; }
    public List<DocumentRecord> DocumentRecords { get; set; } = [];


    public DocumentRecordAuditDeptPageVM(
            List<CompanySelectionVM> companies,
            List<DocumentRecord> docs
        )
    {
        Companies = companies;
        DocumentRecords = docs;
    }
}
