using MediatR;
using Microsoft.Extensions.Logging;
using WorkHive.Domain.Employees;

namespace WorkHive.Application.Employees.EventHandlers
{
    public class UpdateEmployeeEventHandler(ILogger<UpdateEmployeeEventHandler> logger) : INotificationHandler<EmployeeUpdatedDomainEvent>
    {
        private readonly ILogger<UpdateEmployeeEventHandler> _logger = logger;

        public async Task Handle(EmployeeUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EmployeeUpdatedDomainEvent triggered");
            await Task.CompletedTask;
        }
    }
}