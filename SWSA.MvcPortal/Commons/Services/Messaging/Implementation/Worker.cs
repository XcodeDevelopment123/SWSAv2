using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class MessageQueueWorker : BackgroundService
{
    private readonly IMessageConsumer _consumer;

    public MessageQueueWorker(IMessageConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.StartAsync(stoppingToken);
    }
}
// 说明： BackgroundService。适合持续监听内存队列，适配实时或高频任务触发。