using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Employees;

namespace WorkHive.Application.Employees.EventHandlers
{
    public class CreateEmployeeEventHandler(ILogger<CreateEmployeeEventHandler> logger) : INotificationHandler<EmployeeCreatedDomainEvent>
    {
        private readonly ILogger<CreateEmployeeEventHandler> _logger = logger;

        public async Task Handle(EmployeeCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmployeeCreatedDomainEvent fired");
            await Task.CompletedTask;
        }
    }
}