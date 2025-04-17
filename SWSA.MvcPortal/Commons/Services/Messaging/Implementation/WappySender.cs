using Serilog;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class WappySender(HttpClient client) : IMessageSender
{
    public MessagingChannel Channel => MessagingChannel.Wappy;


    public async Task<MessagingResult> SendAsync(MessageEnvelope message)
    {
        try
        {
            var payload = new
            {
                whatsappName = message.Data["whatsappName"],
                number = message.Recipient,
                body = message.Data["body"],
                isGroup = false,
            };

            var response = await client.PostAsJsonAsync("/public/message/send", payload);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var failedMessage = $"Wappy send failed: {response.StatusCode} - {content}";
                Log.Error(failedMessage);
                return new MessagingResult(false, failedMessage);
            }

            var successMessage = $"[Wappy] Sent to {message.Recipient}";
            Log.Information(successMessage);

            return new MessagingResult(true, successMessage);
            ;
        }
        catch (Exception ex)
        {
            var msg = $"[Wappy] exception: {ex.Message}";
            Log.Error(ex, msg);
            return new MessagingResult(false, msg);
        }

    }
}

// 说明：实际调用第三方 WhatsApp API 的 Sender 示例，使用注入的 HttpClient 发出 POST 请求。
