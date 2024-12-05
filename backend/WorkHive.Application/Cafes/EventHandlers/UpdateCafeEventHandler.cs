using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Cafes;

namespace WorkHive.Application.Cafes.EventHandlers
{
    public class UpdateCafeEventHandler(ILogger<CafeUpdatedDomainEvent> logger) : INotificationHandler<CafeUpdatedDomainEvent>
    {
        private readonly ILogger<CafeUpdatedDomainEvent> _logger = logger;

        public async Task Handle(CafeUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(UpdateCafeEventHandler)} - triggered - {notification}");

            await Task.CompletedTask;
        }
    }
}