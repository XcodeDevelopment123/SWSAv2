using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class DefaultDispatcher : IMessageDispatcher
{
    private readonly IEnumerable<IMessageSender> _senders;
    private readonly ITemplateRegistry _templateRegistry;

    public DefaultDispatcher(IEnumerable<IMessageSender> senders, ITemplateRegistry templateRegistry)
    {
        _senders = senders;
        _templateRegistry = templateRegistry;
    }

    public async Task DispatchAsync(MessageEnvelope message)
    {
        if (!_templateRegistry.IsAllowed(message.TemplateCode, message.Channel))
            throw new InvalidOperationException($"Template '{message.TemplateCode}' not allowed on '{message.Channel}'");

        var requiredKeys = _templateRegistry.GetRequiredKeys(message.TemplateCode);
        foreach (var key in requiredKeys)
        {
            if (!message.Data.ContainsKey(key))
                throw new InvalidOperationException($"Missing required template key: '{key}'");
        }

        var sender = _senders.FirstOrDefault(s => s.Channel == message.Channel)
            ?? throw new InvalidOperationException($"Sender not found for channel {message.Channel}");

        var result = await sender.SendAsync(message);
        //Log to system or log
    }
}
// 说明：Dispatcher 负责校验模板通道与参数字段，再派发给 Sender。
