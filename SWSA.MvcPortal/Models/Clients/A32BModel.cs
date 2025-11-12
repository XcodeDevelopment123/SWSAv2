using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.Clients
{
    public class A32BModel
    {
        public int Id { get; set; }

        [Display(Name = "Case No")]
        public string? CaseNo { get; set; }

        [Display(Name = "Date Received")]
        public string? DateReceived { get; set; }

        public string? Client { get; set; }

        [Display(Name = "Officer In Charge")]
        public string? OfficerInCharge { get; set; }

        [Display(Name = "Tel Extension")]
        public string? TelExtension { get; set; }

        [Display(Name = "Year of Assessment")]
        public string? YearAssessment { get; set; }

        [Display(Name = "Date IRB email/letter")]
        public string? DateIRBemailLetter { get; set; }

        [Display(Name = "Details of Correspondence")]
        public string? DetailsCorrepondence { get; set; }

        public string? PIC { get; set; }

        [Display(Name = "Date")]
        public string? Date { get; set; }

        [Display(Name = "Note")]
        public string? Note { get; set; }
    }
}