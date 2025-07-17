using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models.Clients;

namespace SWSA.MvcPortal.Models.DocumentRecords;

public class DocumentRecordAuditDeptPageVM
{
    public List<ClientSelectionVM> Clients { get; set; }
    public List<DocumentRecord> DocumentRecords { get; set; } = [];

    public DocumentRecordAuditDeptPageVM(
            List<ClientSelectionVM> clients,
            List<DocumentRecord> docs
        )
    {
        Clients = clients;
        DocumentRecords = docs;
    }
}
