using SWSA.MvcPortal.Commons.Services.Messaging;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

public interface IMessagingService
{
    Task SendAsync(MessagingChannel channel, string recipient, string templateCode, Dictionary<string, string> data, string? reason);
}

public class MessagingService : IMessagingService
{
    private readonly IMessageProducer _producer;

    public MessagingService(IMessageProducer producer)
    {
        _producer = producer;
    }

    public Task SendAsync(MessagingChannel channel, string recipient, string templateCode, Dictionary<string, string> data, string? reason = "")
    {
        var message = new MessageEnvelope
        {
            Channel = channel,
            Recipient = recipient,
            TemplateCode = templateCode,
            Data = data,
            Reason = reason
        };

        return _producer.EnqueueAsync(message);
    }
}



// 说明：提供给 Controller/API 调用的统一入口。
// Controller 中调用示例：
// await _messagingService.SendAsync(ChannelType.SMS, "+60123456789", "OTP", new() { ["otp"] = "123456" });
