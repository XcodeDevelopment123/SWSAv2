namespace SWSA.MvcPortal.Models.Clients
{
    public class TX5
    {
        public int Id { get; set; }

        // Company Info
        public string CompanyName { get; set; }
        public string TaxReferenceNo { get; set; }
        public string YearEnd { get; set; }
        public string CompanyType { get; set; }
        public string PersonInCharge { get; set; }
        public string PastYearTaxEstimate { get; set; }

        // Initial
        public string Cp204OneMonthBeforeYE { get; set; }
        public string Cp204ReminderDate { get; set; }
        public string Cp204ClientResponse { get; set; }
        public string Cp204CurrentETP { get; set; }
        public string Cp204DateSubmitIRB { get; set; }

        // 1st Revision
        public string Cp204a6thMonthAfterYE { get; set; }
        public string Cp204a1stReminderDate { get; set; }
        public string Cp204a1stClientResponse { get; set; }
        public string Cp204a1stRevisedETP { get; set; }
        public string Cp204a1stDateSubmitIRB { get; set; }

        // 2nd Revision
        public string Cp204a9thMonthAfterYE { get; set; }
        public string Cp204a2ndReminderDate { get; set; }
        public string Cp204a2ndClientResponse { get; set; }
        public string Cp204a2ndRevisedETP { get; set; }
        public string Cp204a2ndDateSubmitIRB { get; set; }

        // 3rd Revision
        public string Cp204a11thMonthAfterYE { get; set; }
        public string Cp204a3rdReminderDate { get; set; }
        public string Cp204a3rdClientResponse { get; set; }
        public string Cp204a3rdRevisedETP { get; set; }
        public string Cp204a3rdDateSubmitIRB { get; set; }
    }
}
