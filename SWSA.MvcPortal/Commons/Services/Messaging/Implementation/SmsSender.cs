using Serilog;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class SmsSender : IMessageSender
{
    public MessagingChannel Channel => MessagingChannel.SMS;

    public async Task<MessagingResult> SendAsync(MessageEnvelope message)
    {
        var successMessage = $"[SMS] To: {message.Recipient} | Template: {message.TemplateCode} | Data: {string.Join(",", message.Data)}";
        Log.Information(successMessage);


        await Task.Delay(500); // delay
        return new MessagingResult(true, successMessage, message);
    }
}

// 说明：SMS Sender 示例。真实实现中应调用 SMS 网关 API（如 Twilio、MSG91、iSMS）。
