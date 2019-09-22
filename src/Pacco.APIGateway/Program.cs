using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Metrics.AppMetrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntrada;
using Ntrada.Extensions.RabbitMq;
using Ntrada.Hooks;
using Pacco.APIGateway.Infrastructure;

namespace Pacco.APIGateway
{
    public static class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    var configPath = args?.FirstOrDefault() ?? "ntrada.yml";
                    builder.AddYamlFile(configPath, false);
                })
                .ConfigureServices(services => services.AddNtrada()
                    .AddOpenTracing()
                    .AddSingleton<IContextBuilder, CorrelationContextBuilder>()
                    .AddSingleton<IBeforeHttpClientRequestHook, CorrelationContextHttpHook>()
                    .AddConvey()
                    .AddMetrics())
                .Configure(app => app.UseNtrada())
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
