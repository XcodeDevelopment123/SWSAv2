namespace SWSA.MvcPortal.Commons.Services.Messaging.Models;

public class WappyMessageRequest
{
    public string Recipient { get; set; } = default!;
    public string Message { get; set; } = default!;
}
