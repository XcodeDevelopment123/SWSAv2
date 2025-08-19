using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.Templates;

public class TaxStrikeOffTemplate
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal? IRBPenaltiesAmount { get; set; }
    public DateTime? PenaltiesAppealDate { get; set; }
    public DateTime? PenaltiesPaymentDate { get; set; }
    public string? Remarks { get; set; }
    public bool IsAccountWorkComplete { get; set; } = false;
    public DateTime? FormESubmitDate { get; set; }
    public DateTime? FormCSubmitDate { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public decimal? InvoiceAmount { get; set; }
    public bool IsClientCopySent { get; set; } = false;

    [ForeignKey(nameof(ClientId))]
    public BaseCompany Client { get; set; } = null!;
}
