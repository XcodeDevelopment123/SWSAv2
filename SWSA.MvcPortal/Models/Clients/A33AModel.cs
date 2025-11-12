using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Models.DocumentRecords
{
    public class A33AModel
    {
        public int Id { get; set; }

        [Display(Name = "Case No")]
        public string? CaseNo { get; set; }

        [Display(Name = "Date Received")]
        public string? DateReceived { get; set; }

        [Display(Name = "Type of Incoming")]
        public string? TypeIncoming { get; set; }

        [Display(Name = "Client")]
        public string? Client { get; set; }

        [Display(Name = "Year of Assessment")]
        public string? YearAssessment { get; set; }

        [Display(Name = "Details")]
        public string? Details { get; set; }

        [Display(Name = "Date")]
        public string? Date { get; set; }

        [Display(Name = "Brief Descriptions")]
        public string? BriefDescritions { get; set; }

        [Display(Name = "PIC")]
        public string? PIC { get; set; }

        [Display(Name = "Remark")]
        public string? Remark { get; set; }

        [Display(Name = "Done On")]
        public string? DoneOn { get; set; }
    }
}