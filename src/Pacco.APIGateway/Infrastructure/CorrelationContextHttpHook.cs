using System.Net.Http;
using Newtonsoft.Json;
using Ntrada;
using Ntrada.Handlers.RabbitMq;
using Ntrada.Hooks;

namespace Pacco.APIGateway.Infrastructure
{
    internal sealed class CorrelationContextHttpHook : IBeforeHttpClientRequestHook
    {
        private readonly IContextBuilder _contextBuilder;

        public CorrelationContextHttpHook(IContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        public void Invoke(HttpClient client, ExecutionData data)
        {
            var context = JsonConvert.SerializeObject(_contextBuilder.Build(data));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Correlation-Context", context);
        }
    }
}