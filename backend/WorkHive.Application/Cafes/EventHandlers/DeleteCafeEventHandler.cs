using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Cafes;

namespace WorkHive.Application.Cafes.EventHandlers
{
    public class DeleteCafeEventHandler(ILogger<DeleteCafeEventHandler> logger) : INotificationHandler<CafeDeletedDomainEvent>
    {
        private readonly ILogger<DeleteCafeEventHandler> _logger = logger;

        public async Task Handle(CafeDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(DeleteCafeEventHandler)} - triggered - {notification}");
            await Task.CompletedTask;
        }
    }
}