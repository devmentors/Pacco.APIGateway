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

namespace Pacco.APIGateway
{
    public static class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddOpenTracing()
                    .AddConvey()
                    .AddMetrics()
                    .Build())
                .UseNtrada()
                .UseRabbitMq()
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
