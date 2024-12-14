using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WorkHive.Application.Abstraction.Behaviors;
using WorkHive.Application.Employees.Commands.Create;

namespace WorkHive.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommand>();
            services.AddFluentValidationAutoValidation();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly));

            // Register Pipeline Behaviors
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryPolicyBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            return services;
        }
    }
}