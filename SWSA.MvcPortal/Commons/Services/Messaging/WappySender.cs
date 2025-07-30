using Microsoft.Extensions.Options;
using Serilog;
using SWSA.MvcPortal.Commons.Services.Messaging.Configs;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Models;

namespace SWSA.MvcPortal.Commons.Services.Messaging;

public class WappySender(
IHttpClientFactory clientFactory,
    IOptions<WappySettings> options
    )
{
    private readonly HttpClient _client = clientFactory.CreateClient("WappyClient");
    private readonly MessagingChannel _channel = MessagingChannel.Wappy;
    private readonly WappySettings _settings = options.Value;

    public async Task<MessagingResult> SendAsync(WappyMessageRequest req)
    {
        var messagingResult = new MessagingResult(req.Recipient,_channel);
        try
        {
            var payload = new
            {
                whatsappName = _settings.WhatsappName,
                number = req.Recipient,
                body = req.Message,
                isGroup = false,
            };

            var response = await _client.PostAsJsonAsync("/public/message/send", payload);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var failedMessage = $"[Wappy] send failed: {response.StatusCode} - {content}";
                Log.Error(failedMessage);

                messagingResult.SetFailure(failedMessage);
                return messagingResult;
            }

            var successMessage = $"[Wappy] Sent to {req.Recipient}";
            Log.Information(successMessage);
            messagingResult.SetSuccess(successMessage);
            return messagingResult;
        }
        catch (Exception ex)
        {
            var msg = $"[Wappy] exception: {ex.Message}";
            Log.Error(ex, msg);
            messagingResult.SetFailure(msg);
            return messagingResult;
        }
    }
}
