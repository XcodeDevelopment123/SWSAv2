using SWSA.MvcPortal.Commons.Services.Messaging.Intefaces;
using System.Threading.Channels;

namespace SWSA.MvcPortal.Commons.Services.Messaging.Implementation;

public class InMemoryMessageQueue : IMessageProducer, IMessageConsumer
{
    private readonly Channel<MessageEnvelope> _channel = Channel.CreateUnbounded<MessageEnvelope>();
    private readonly IMessageDispatcher _dispatcher;

    public InMemoryMessageQueue(IMessageDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public Task EnqueueAsync(MessageEnvelope message)
    {
        return _channel.Writer.WriteAsync(message).AsTask();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await foreach (var message in _channel.Reader.ReadAllAsync(cancellationToken))
        {
            await _dispatcher.DispatchAsync(message);
        }
    }
}

// 说明：MVP 阶段使用 Channel<T> 实现队列（无外部依赖）。未来可换成 Redis/RabbitMQ。