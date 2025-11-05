using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class DocumentRecord
{
    public int Id { get; set; }

    public string Department { get; set; } = null!;

    public DateTime DocumentDate { get; set; }

    public int DocumentFlow { get; set; }

    public int DocumentType { get; set; }

    public int BagOrBoxCount { get; set; }

    public string UploadLetter { get; set; } = null!;

    public string? Remark { get; set; }

    public int HandledByStaffId { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual User HandledByStaff { get; set; } = null!;
}
