using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.SecretaryDept;

public class SecStrikeOffTemplate
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? CompleteDate { get; set; }

    public int? DoneByUserId { get; set; }
    public decimal? PenaltiesAmount { get; set; }
    public decimal? RevisedPenaltiesAmount { get; set; }
    public DateTime? SSMPenaltiesAppealDate { get; set; }
    public DateTime? SSMPenaltiesPaymentDate { get; set; }
    public DateTime? SSMDocSentDate { get; set; }
    public DateTime? SSMSubmissionDate { get; set; }
    public string? Remarks { get; set; }

    [ForeignKey(nameof(DoneByUserId))]
    public User DoneBy { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    public BaseCompany Client { get; set; } = null!;
}
