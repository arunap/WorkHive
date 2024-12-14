using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WorkHive.Application.Abstraction.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse>(ILogger<PerformanceBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger = logger;
        private readonly Stopwatch _timer = new Stopwatch();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                _logger.LogWarning("Long Running Request: {RequestName} ({ElapsedMilliseconds} ms)", typeof(TRequest).Name, elapsedMilliseconds);
            }

            return response;
        }
    }
}