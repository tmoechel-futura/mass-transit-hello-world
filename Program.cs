using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitGettingStarted
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        //x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        // x.AddSagaStateMachines(entryAssembly);
                        // x.AddSagas(entryAssembly);
                        // x.AddActivities(entryAssembly);

                        // *********** In Memory ***********************
                        // x.UsingInMemory((context, cfg) =>
                        // {
                        //     cfg.ConfigureEndpoints(context);
                        // });
                        // **********************************************
                        
                        //*********** Azure Service Bus ********************
                        x.UsingAzureServiceBus((context,cfg) =>
                        {
                            cfg.Host("Endpoint=sb://tm-servicebus.servicebus.windows.net/;SharedAccessKeyName=mt-servicebus-access;SharedAccessKey=qUyJWroD4wkcuNV1hzenUB3wW2aW5OTOUr2o+DLh8Rc=");
                        
                            cfg.ConfigureEndpoints(context);
                        });
                        
                        // *********** RabbitMQ ****************************
                       //  x.UsingRabbitMq((cxt, cfg) =>
                       //  {
                       //     cfg.Host("localhost", "/", h =>
                       //     {
                       //         h.Username("guest");
                       //         h.Password("guest");
                       //     });
                       //     cfg.ConfigureEndpoints(cxt);
                       // });
                       
                    });
                    // Configuration options
                    // services.AddOptions<MassTransitHostOptions>()
                    //     .Configure(options =>
                    //     {
                    //         // if specified, waits until the bus is started before
                    //         // returning from IHostedService.StartAsync
                    //         // default is false
                    //         options.WaitUntilStarted = true;
                    //
                    //         // if specified, limits the wait time when starting the bus
                    //         options.StartTimeout = TimeSpan.FromSeconds(10);
                    //
                    //         // if specified, limits the wait time when stopping the bus
                    //         options.StopTimeout = TimeSpan.FromSeconds(30);
                    //     });
                    services.AddHostedService<Worker>();
                });
    }
}
