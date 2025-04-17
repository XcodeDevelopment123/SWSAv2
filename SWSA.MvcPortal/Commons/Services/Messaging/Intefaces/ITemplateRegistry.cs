using SWSA.MvcPortal.Commons.Services.Messaging.Enums;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

public interface ITemplateRegistry
{
    bool IsAllowed(string templateCode, MessagingChannel channel);
    IReadOnlyCollection<string> GetRequiredKeys(string templateCode, MessagingChannel channel);
}

// 说明：模板校验器接口。用于控制哪些模板能在特定通道使用，避免发送不支持的内容。
