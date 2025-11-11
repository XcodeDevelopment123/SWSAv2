using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Data.Models;

public partial class S15
{
    public int Id { get; set; }

    public string? SecFileNo { get; set; }

    public string? CompanyName { get; set; }

    public string? YearEnd { get; set; }

    public string? IncorpDate { get; set; }

    public string? Co { get; set; }

    public string? CompanyStatus { get; set; }

    public string? ActiveCoActivitySize { get; set; }

    public string? SsmextensionDateforAcc { get; set; }

    public string? AddueDate { get; set; }

    public string? AdsubmitDate { get; set; }

    public string? DateSendtoClient { get; set; }

    public string? DateReturned { get; set; }

    public string? JobCompleted { get; set; }
}
