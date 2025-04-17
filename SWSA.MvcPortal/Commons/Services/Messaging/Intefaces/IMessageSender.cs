using SWSA.MvcPortal.Commons.Services.Messaging.Enums;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

public interface IMessageSender
{
    MessagingChannel Channel { get; }
    Task<MessagingResult> SendAsync(MessageEnvelope message);
}

// 说明：Sender 接口。每种通道（SMS、Email）各自实现，用于实际发送逻辑。