using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Contracts
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection service)
        {
            service.AddMassTransit(bus =>
            {                
                bus.UsingRabbitMq((ctx, config) =>
                {
                    var Configuration = ctx.GetService<IConfiguration>();
                    config.Host(Configuration.GetConnectionString("RabbitMqConnection"));
                    config.ConfigureEndpoints(ctx);
                });
                bus.AddConsumers(Assembly.GetEntryAssembly());
            });
            return service;

        }
    }
}
