using Serilog;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class SmsSender : IMessageSender
{
    public MessagingChannel Channel => MessagingChannel.SMS;

    public async Task<MessagingResult> SendAsync(MessageEnvelope message)
    {
        var successMessage = $"[Email] To: {message.Recipient} | Template: {message.TemplateCode} | Data: {string.Join(",", message.Data.Select(kv => $"{kv.Key}={kv.Value}"))}";
        Log.Information(successMessage);

        await Task.Delay(500);
        
        return new MessagingResult(true, successMessage, message);
    }
}

