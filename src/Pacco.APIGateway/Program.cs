using System;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Metrics.AppMetrics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ntrada;
using Ntrada.Extensions.RabbitMq;
using Ntrada.Hooks;
using Pacco.APIGateway.Infrastructure;

namespace Pacco.APIGateway
{
    public static class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(builder =>
                        {
                            var ntradaConfig = Environment.GetEnvironmentVariable("NTRADA_CONFIG");
                            var configPath = args?.FirstOrDefault() ?? ntradaConfig ?? "ntrada.yml";
                            builder.AddYamlFile(configPath, false);
                        })
                        .ConfigureServices(services => services.AddNtrada()
                            .AddOpenTracing()
                            .AddSingleton<IContextBuilder, CorrelationContextBuilder>()
                            .AddSingleton<IBeforeHttpClientRequestHook, CorrelationContextHttpHook>()
                            .AddConvey()
                            .AddMetrics())
                        .Configure(app => app.UseNtrada())
                        .UseLogging();
                });
    }
}
