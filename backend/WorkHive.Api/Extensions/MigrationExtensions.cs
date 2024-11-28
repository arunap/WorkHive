using Microsoft.EntityFrameworkCore;
using WorkHive.Infrastructure.Database;

namespace WorkHive.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}