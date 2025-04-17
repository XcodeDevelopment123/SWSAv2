namespace SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

public interface IMessageConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
}

// 说明：消费者接口。后台服务实现此接口以异步读取队列并处理消息。