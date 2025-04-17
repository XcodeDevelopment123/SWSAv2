using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class DefaultDispatcher : IMessageDispatcher
{
    private readonly IEnumerable<IMessageSender> _senders;
    private readonly ITemplateRegistry _templateRegistry;
    private readonly IServiceScopeFactory _scopeFactory;

    public DefaultDispatcher(
        IEnumerable<IMessageSender> senders,
        ITemplateRegistry templateRegistry,
        IServiceScopeFactory scopeFactory)
    {
        _senders = senders;
        _templateRegistry = templateRegistry;
        _scopeFactory = scopeFactory;
    }

    public async Task DispatchAsync(MessageEnvelope message)
    {
        if (!_templateRegistry.IsAllowed(message.TemplateCode, message.Channel))
            throw new InvalidOperationException($"Template '{message.TemplateCode}' not allowed on '{message.Channel}'");

        var requiredKeys = _templateRegistry.GetRequiredKeys(message.TemplateCode, message.Channel);
        foreach (var key in requiredKeys)
        {
            if (!message.Data.ContainsKey(key))
                throw new InvalidOperationException($"Missing required template key: '{key}'");
        }

        var sender = _senders.FirstOrDefault(s => s.Channel == message.Channel)
            ?? throw new InvalidOperationException($"Sender not found for channel {message.Channel}");

        var result = await sender.SendAsync(message);

        using var scope = _scopeFactory.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ISystemNotificationLogService>();
        await logger.AddLog(result);
        //Log to system or log
    }
}
// 说明：Dispatcher 负责校验模板通道与参数字段，再派发给 Sender。
