using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorkHive.Api
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // ensures that all generated URLs are lowercase.
            services.AddRouting(options => options.LowercaseUrls = true);

            return services;
        }
    }
}