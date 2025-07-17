using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum ServiceScope
{
    Error=0,
    Sec = 10,
    Audit = 11,
    Tax = 12,
    Acc = 13,
    [Display(Name = "Form E")]
    FormE = 14,
    [Display(Name = "Form BE")]
    FormBE = 15,
    [Display(Name = "Form B & P")]
    FormB_P = 16,

    //Here is other service, Use int 20>= for recognize other service
    [Display(Name = "PCB")]
    OthersPCB = 20,
    [Display(Name = "Monthly E Invoicing")]
    OthersMonthlyEInvoice = 21,
    [Display(Name = "Not Sec Filing Only")]
    NotSecFilingOnly = 22,
    [Display(Name = "Review & Tax")]
    ReviewAndTax = 23,

}
