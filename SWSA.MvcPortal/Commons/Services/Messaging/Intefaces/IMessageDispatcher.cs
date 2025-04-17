namespace SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

public interface IMessageDispatcher
{
    Task DispatchAsync(MessageEnvelope message);
}

// 说明：消息分发器接口。根据通道调用相应的 Sender。