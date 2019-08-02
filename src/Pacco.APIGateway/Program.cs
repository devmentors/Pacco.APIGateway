using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Metrics.AppMetrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ntrada;
using Ntrada.Handlers.RabbitMq;
using Ntrada.Hooks;
using Pacco.APIGateway.Infrastructure;

namespace Pacco.APIGateway
{
    public static class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddOpenTracing()
                    .AddSingleton<IContextBuilder, CorrelationContextBuilder>()
                    .AddSingleton<IBeforeHttpClientRequestHook, CorrelationContextHttpHook>()
                    .AddRabbitMq<CorrelationContext>()
                    .AddNtrada()
                    .AddConvey()
                    .AddMetrics())
                .Configure(app => app
                    .UseRabbitMq()
                    .UseNtrada())
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
