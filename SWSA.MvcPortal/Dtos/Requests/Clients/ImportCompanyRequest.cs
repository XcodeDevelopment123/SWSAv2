namespace SWSA.MvcPortal.Dtos.Requests.Clients;

/// <summary>
/// One row from the Excel import file. All values are kept as raw strings
/// so the server can validate / parse them and report row-level errors.
/// </summary>
public class ImportCompanyRow
{
    public int RowNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? EmployerNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? IncorporationDate { get; set; }
    public string? YearEndMonth { get; set; }
    public string? ActivitySize { get; set; }
    /// <summary>Optional for Enterprise (SoleProprietorships / Partnership) or other types</summary>
    public string? CompanyType { get; set; }
    /// <summary>MSIC codes separated by comma / semicolon, e.g. "62010, 69200" (max 3)</summary>
    public string? MsicCodes { get; set; }
    /// <summary>Group name (must match an active group)</summary>
    public string? Group { get; set; }
    public string? Referral { get; set; }
    public string? FileNo { get; set; }
}

public class ImportCompanyRequest
{
    public List<ImportCompanyRow> Rows { get; set; } = [];
}

public class ImportCompanyRowResult
{
    public int RowNumber { get; set; }
    public string? CompanyName { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? ClientId { get; set; }
}

public class ImportCompanyResult
{
    public int Total { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public List<ImportCompanyRowResult> Results { get; set; } = [];
}
