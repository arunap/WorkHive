using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;

namespace WorkHive.Infrastructure.Providers
{
    public sealed class EmployeeIdProvider(IApplicationDbContext context) : IEmployeeIdProvider
    {
        private readonly IApplicationDbContext _context = context;

        private const string Prefix = "UI";
        private const int Length = 7;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        public async Task<string> NextIdAsync()
        {
            string employeeId;
            do
            {
                employeeId = GenerateEmployeeId();

            } while (await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId) != null);

            return employeeId;
        }

        private static string GenerateEmployeeId()
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < Length; i++)
                result.Append(Characters[random.Next(Characters.Length)]);

            return $"{Prefix}{result}";
        }
    }
}