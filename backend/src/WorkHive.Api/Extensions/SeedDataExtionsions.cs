using WorkHive.Infrastructure.Database.Seeds;

namespace WorkHive.Api.Extensions
{
    public static class SeedDataExtionsions
    {
        public static async Task ApplySeedDataAsync(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var cafeSvc = scope.ServiceProvider.GetRequiredService<CafeDataInitializer>();
            await cafeSvc.SeedAsync();

            var employeeSvc = scope.ServiceProvider.GetRequiredService<EmployeeDataInitializer>();
            await employeeSvc.SeedAsync();
        }
    }
}