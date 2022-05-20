using System.Threading.Tasks;
using MassTransit;
using MassTransitGettingStarted.Contracts;
using Microsoft.Extensions.Logging;

namespace MassTransitGettingStarted.Consumers
{
    public class MassTransitGettingStartedConsumer : IConsumer<HelloMessage>
    {
        private readonly ILogger<MassTransitGettingStartedConsumer> _logger;

        public MassTransitGettingStartedConsumer(ILogger<MassTransitGettingStartedConsumer> logger)
        {
            _logger = logger;
        }
        
        public Task Consume(ConsumeContext<HelloMessage> context)
        {
            _logger.LogInformation("Hello and Welcome {Name}", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}