using MediatR;
using Polly;

namespace WorkHive.Application.Abstraction.Behaviors
{
    public class RetryPolicyBehavior<TRequest, TResponse>(IAsyncPolicy<TResponse> retryPolicy) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IAsyncPolicy<TResponse> _retryPolicy = retryPolicy;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return _retryPolicy.ExecuteAsync(() => next());
        }
    }
}