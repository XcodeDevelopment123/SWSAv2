using System;
using System.Collections.Generic;

namespace SWSA.MvcPortal.Models.Entities;

public partial class AuditTemplate
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int? PersonInChargeId { get; set; }

    public int Database { get; set; }

    public int Status { get; set; }

    public int QuarterToDo { get; set; }

    public DateTime YearEndToDo { get; set; }

    public decimal? Revenue { get; set; }

    public decimal? Profit { get; set; }

    public decimal? AuditFee { get; set; }

    public DateTime? DateBilled { get; set; }

    public DateTime AuditStartDate { get; set; }

    public DateTime AuditEndDate { get; set; }

    public int TotalFieldWorkDays { get; set; }

    public int? AuditWipresult { get; set; }

    public bool IsAccSetupComplete { get; set; }

    public bool IsAccSummaryComplete { get; set; }

    public bool IsAuditPlanningComplete { get; set; }

    public bool IsAuditExecutionComplete { get; set; }

    public bool IsExecutionAuditComplete { get; set; }

    public DateTime? FirstReviewSendDate { get; set; }

    public DateTime? FirstReviewEndDate { get; set; }

    public int? FirstReviewResult { get; set; }

    public DateTime? SecondReviewSendDate { get; set; }

    public DateTime? SecondReviewEndDate { get; set; }

    public int? SecondReviewResult { get; set; }

    public DateTime? KualaLumpurOfficeDateSent { get; set; }

    public DateTime? KualaLumpurOfficeAuditReportReceivedDate { get; set; }

    public DateTime? KualaLumpurOfficeReportDate { get; set; }

    public DateTime? KualaLumpurOfficeDirectorsReportDate { get; set; }

    public DateTime? DirectorDateSent { get; set; }

    public DateTime? DirectorFollowUpDate { get; set; }

    public DateTime? DirectorDateReceived { get; set; }

    public DateTime? DirectorCommOfOathsDate { get; set; }

    public DateTime? TaxDueDate { get; set; }

    public DateTime? DatePassToTaxDept { get; set; }

    public DateTime? SecSsmdueDate { get; set; }

    public DateTime? DatePassToSecDept { get; set; }

    public DateTime? PostAuditDateBinded { get; set; }

    public DateTime? PostAuditDespatchDateToClient { get; set; }

    public virtual BaseCompany Client { get; set; } = null!;

    public virtual User? PersonInCharge { get; set; }
}
