using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Cafes;

namespace WorkHive.Application.Cafes.EventHandlers
{
    public class CreateCafeEventHandler(ILogger<CreateCafeEventHandler> logger) : INotificationHandler<CafeCreatedDomainEvent>
    {
        private readonly ILogger<CreateCafeEventHandler> _logger = logger;

        public async Task Handle(CafeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CreateCafeEventHandler)} - triggered - {notification}");

            await Task.CompletedTask;
        }
    }
}