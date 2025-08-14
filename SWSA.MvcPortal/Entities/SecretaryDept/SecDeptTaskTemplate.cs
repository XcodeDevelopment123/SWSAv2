using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities.Clients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWSA.MvcPortal.Entities.SecretaryDept;

//Sdn Bhd AR
//LLP AD

public class SecDeptTaskTemplate
{
    [Key]
    public int Id { get; set; }
    public int ClientId { get; set; }

    public DateTime? ARDueDate { get; set; }
    public DateTime? ARSubmitDate { get; set; }
    public DateTime? ARSendToClientDate { get; set; }
    public DateTime? ARReturnByClientDate { get; set; }

    public DateTime? ADSubmitDate { get; set; }
    public DateTime? ADSendToClientDate { get; set; }
    public DateTime? ADReturnByClientDate { get; set; }
    public string? Remarks { get; set; } = string.Empty;

    [ForeignKey(nameof(ClientId))]
    public BaseCompany Client { get; set; } = null!;
}
