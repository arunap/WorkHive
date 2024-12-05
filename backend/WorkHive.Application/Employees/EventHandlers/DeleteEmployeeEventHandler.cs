using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Employees;

namespace WorkHive.Application.Employees.EventHandlers
{
    public class DeleteEmployeeEventHandler(ILogger<DeleteEmployeeEventHandler> logger) : INotificationHandler<EmployeeDeletedDomainEvent>
    {
        private readonly ILogger<DeleteEmployeeEventHandler> _logger = logger;

        public async Task Handle(EmployeeDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmployeeDeletedDomainEvent fired");
            await Task.CompletedTask;
        }
    }
}