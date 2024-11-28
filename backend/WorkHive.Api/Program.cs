using WorkHive.Api;
using WorkHive.Api.Extensions;
using WorkHive.Application;
using WorkHive.Infrastructure;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplication()
            .AddPresentation()
            .AddInfrastructure(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "React.Client", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) { }

        await app.ApplyMigrationsAsync();

        await app.ApplySeedDataAsync();

        app.UseSwaggerWithUI();

        app.UseStaticFiles();

        app.UseCors("React.Client");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}