namespace SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;


public interface IMessageProducer
{
    Task EnqueueAsync(MessageEnvelope message);
}

// 说明：生产者接口。由前端或系统逻辑调用，将消息推送进队列。