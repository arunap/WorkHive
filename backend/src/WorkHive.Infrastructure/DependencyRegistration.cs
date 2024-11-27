using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Infrastructure.Database;
using WorkHive.Infrastructure.Database.Seeds;
using WorkHive.Infrastructure.Providers;
using WorkHive.Infrastructure.Shared;

namespace WorkHive.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration);

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CafeDataInitializer>();
            services.AddScoped<EmployeeDataInitializer>();
            services.AddScoped<IEmployeeIdProvider, EmployeeIdProvider>();
            services.AddScoped<IImageUploader, ImageUploader>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(connectionString,
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

            return services;
        }

    }
}