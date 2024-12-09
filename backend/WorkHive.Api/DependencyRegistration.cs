using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WorkHive.Api.Context;
using WorkHive.Api.Dtos;
using WorkHive.Application.Abstraction.Context;

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
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(m => m.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                    var response = new ErrorDto
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Validation errors occurred",
                        Errors = [.. errors],
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // ensures that all generated URLs are lowercase.
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddScoped<IUserContext, UserContextProvider>();

            return services;
        }
    }
}