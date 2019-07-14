using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ntrada;
using Ntrada.Extensions.RabbitMq;

namespace Pacco.APIGateway
{
    public static class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(s => s.AddOpenTracing())
                .UseNtrada()
                .UseRabbitMq()
                .Build()
                .RunAsync();
    }
}
