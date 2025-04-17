using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class InMemoryTemplateRegistry : ITemplateRegistry
{
    private readonly Dictionary<string, HashSet<MessagingChannel>> _registry = new()
    {
        [MessagingTemplateCode.OTP] = new() { MessagingChannel.SMS, MessagingChannel.Email },
        [MessagingTemplateCode.Notification] = new() { MessagingChannel.Email, MessagingChannel.WhatsApp },
        [MessagingTemplateCode.Wappy] = new() { MessagingChannel.Wappy },
    };

    //Use template data to define at usage
    private readonly Dictionary<string, string[]> _templateRequiredKeys = new()
    {
        [MessagingTemplateCode.OTP] = new[] { "otp" },
        [MessagingTemplateCode.Notification] = new[] { "title", "message" },
        [MessagingTemplateCode.Wappy] = new[] { "whatsappName", "body" },
    };


    public bool IsAllowed(string templateCode, MessagingChannel channel)
    {
        return _registry.TryGetValue(templateCode, out var allowed) && allowed.Contains(channel);
    }

    public IReadOnlyCollection<string> GetRequiredKeys(string templateCode)
    {
        return _templateRequiredKeys.TryGetValue(templateCode, out var keys) ? keys : Array.Empty<string>();
    }
}

// 说明：内存版模板注册器。未来可替换为数据库或配置文件加载方式。