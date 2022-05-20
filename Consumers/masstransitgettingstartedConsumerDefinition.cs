using MassTransitGettingStarted.Consumers;

namespace Company.Consumers
{
    using MassTransit;

    public class masstransitgettingstartedConsumerDefinition :
        ConsumerDefinition<MassTransitGettingStartedConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MassTransitGettingStartedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}