using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Services.Messaging.Enums;
using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class InMemoryTemplateRegistry : ITemplateRegistry
{
    private readonly Dictionary<string, HashSet<MessagingChannel>> _templateChannelSupport = new()
    {
        [MessagingTemplateCode.AssignmentWorkDueSoon] = new() { MessagingChannel.Email, MessagingChannel.Wappy },
        [MessagingTemplateCode.OTP] = new() { MessagingChannel.SMS, MessagingChannel.Email },
        [MessagingTemplateCode.Notification] = new() { MessagingChannel.Email, MessagingChannel.WhatsApp },
    };

    private readonly Dictionary<(string TemplateCode, MessagingChannel Channel), string[]> _templateChannelRequiredKeys = new()
    {
        [(MessagingTemplateCode.AssignmentWorkDueSoon, MessagingChannel.Email)] = new[] { "title", "content", "footer" },
        [(MessagingTemplateCode.AssignmentWorkDueSoon, MessagingChannel.WhatsApp)] = new[] { "body" },
        [(MessagingTemplateCode.OTP, MessagingChannel.SMS)] = new[] { "otp" },
        [(MessagingTemplateCode.OTP, MessagingChannel.Email)] = new[] { "otp", "emailSubject" },
        [(MessagingTemplateCode.Notification, MessagingChannel.Email)] = new[] { "title", "message" },
        [(MessagingTemplateCode.Notification, MessagingChannel.WhatsApp)] = new[] { "whatsappName", "message" },
    };

    public bool IsAllowed(string templateCode, MessagingChannel channel)
    {
        return _templateChannelSupport.TryGetValue(templateCode, out var allowed) && allowed.Contains(channel);
    }

    public IReadOnlyCollection<string> GetRequiredKeys(string templateCode, MessagingChannel channel)
    {
        return _templateChannelRequiredKeys.TryGetValue((templateCode, channel), out var keys)
            ? keys
            : Array.Empty<string>();
    }
}