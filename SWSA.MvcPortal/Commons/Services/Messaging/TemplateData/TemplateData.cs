namespace SWSA.MvcPortal.Commons.Services.Messaging.TemplateData;

public class OtpTemplateData
{
    public string Otp { get; set; } = null!;
}

public class NotificationTemplateData
{
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
}

public class WappyTemplateData
{
    public string WhatsappName { get; set; } = null!;
    public string Body { get; set; } = null!;
}
