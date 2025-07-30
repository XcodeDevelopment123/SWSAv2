using SWSA.MvcPortal.Commons.Services.Messaging.Enums;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Models;

public class MessagingResult
{
    public bool IsSuccess { get; set; } = false;
    public string Recipient { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public MessagingChannel Channel { get; set; } = MessagingChannel.Unknown;

    public MessagingResult(string recipient,MessagingChannel channel)
    {
        Recipient = recipient;
        Channel = channel;
    }

    public void SetSuccess(string Message)
    {
        IsSuccess = true;
        this.Message = Message;
    }

    public void SetFailure(string Message)
    {
        IsSuccess = false;
        this.Message = Message;
    }
}
