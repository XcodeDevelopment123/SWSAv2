using System.ComponentModel.DataAnnotations;

namespace SWSA.MvcPortal.Commons.Enums;

public enum ReminderType
{
    [Display(Name = "Email Notification")]
    EmailNotification = 1,

    [Display(Name = "Phone Call Reminder")]
    PhoneCallReminder = 2,

    [Display(Name = "SMS/Text Message")]
    TextMessage = 3,

    [Display(Name = "System Notification")]
    SystemNotification = 4,

    [Display(Name = "Meeting Reminder")]
    MeetingReminder = 5,

    [Display(Name = "Document Collection")]
    DocumentCollection = 6,

    [Display(Name = "Start Work Preparation")]
    StartWorkPreparation = 7,

    [Display(Name = "Due Date Reminder")]
    DueDateReminder = 8,

    [Display(Name = "Progress Follow Up")]
    ProgressFollowUp = 9,

    [Display(Name = "Handover Notice")]
    HandoverNotice = 10,

    [Display(Name = "Filing Preparation")]
    FilingPreparation = 11,

    [Display(Name = "Final Review")]
    FinalReview = 12,

    [Display(Name = "Tax Planning")]
    TaxPlanning = 13,

    [Display(Name = "Anniversary Reminder")]
    AnniversaryReminder = 14,

    [Display(Name = "AGM & AFS Due")]
    AGMAFSDue = 15
}

public enum ReminderStatus
{
    [Display(Name = "Pending")]
    Pending = 1,
    
    [Display(Name = "Sent")]
    Sent = 2,
    
    [Display(Name = "Acknowledged")]
    Acknowledged = 3,
    
    [Display(Name = "Snoozed")]
    Snoozed = 4,
    
    [Display(Name = "Cancelled")]
    Cancelled = 5
}

public enum ReminderChannel
{
    [Display(Name = "Email")]
    Email = 1, 
    [Display(Name = "SMS")]
    SMS, 
    [Display(Name = "WhatsApp")]
    WhatsApp ,
    [Display(Name = "System Notification")]
    SystemNotification ,

    Text = 10,
    [Display(Name = "Text/Call")]
    TextorCall,

    Call =20,
    [Display(Name = "Call/Reminder")]
    CallorReminder,
    [Display(Name = "Call/Email")]
    CallorEmail,

    Meeting=30,
}