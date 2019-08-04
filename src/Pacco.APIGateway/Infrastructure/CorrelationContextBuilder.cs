using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Ntrada;
using Ntrada.Handlers.RabbitMq;
using OpenTracing;

namespace Pacco.APIGateway.Infrastructure
{
    internal sealed class CorrelationContextBuilder : IContextBuilder
    {
        private readonly NtradaConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public CorrelationContextBuilder(NtradaConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public object Build(ExecutionData executionData)
        {
            var spanContext = string.Empty;
            if (_configuration.UseJaeger)
            {
                var tracer = _serviceProvider.GetService<ITracer>();
                spanContext = tracer is null ? string.Empty :
                    tracer.ActiveSpan is null ? string.Empty : tracer.ActiveSpan.Context.ToString();
            }

            var name = string.Empty;
            if (!(executionData.Route.Config is null) &&
                executionData.Route.Config.TryGetValue("routing_key", out var routingKey))
            {
                name = routingKey ?? string.Empty;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                name = $"{executionData.Request.Method} {executionData.Request}";
            }

            return new CorrelationContext
            {
                CorrelationId = executionData.RequestId,
                User = new CorrelationContext.UserContext
                {
                    Id = executionData.UserId,
                    Claims = executionData.Claims,
                    Role = executionData.Claims.FirstOrDefault(c => c.Key == ClaimTypes.Role).Value?.ToLowerInvariant(),
                    IsAuthenticated = !string.IsNullOrWhiteSpace(executionData.UserId)
                },
                ResourceId = executionData.ResourceId,
                TraceId = executionData.TraceId,
                ConnectionId = executionData.Request.HttpContext.Connection.Id,
                Name = name,
                CreatedAt = DateTime.UtcNow,
                SpanContext = spanContext
            };
        }
    }
}