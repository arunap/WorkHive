using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Employees;

namespace WorkHive.Infrastructure.Database.Seeds
{
    public class EmployeeDataInitializer(IApplicationDbContext context, IEmployeeIdProvider idProvider)
    {
        private static readonly List<Employee> employees =
        [
            new Employee { Name = "Alice Smith", EmailAddress = "alice.smith@example.com", PhoneNumber = "87289722", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Bob Johnson", EmailAddress = "bob.johnson@example.com", PhoneNumber = "94429350", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Charlie Brown", EmailAddress = "charlie.brown@example.com", PhoneNumber = "91062378", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Diana Prince", EmailAddress = "diana.prince@example.com", PhoneNumber = "97668551", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Edward Davis", EmailAddress = "edward.davis@example.com", PhoneNumber = "93993258", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Fiona Green", EmailAddress = "fiona.green@example.com", PhoneNumber = "9137758", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "George Harris", EmailAddress = "george.harris@example.com", PhoneNumber = "88241695", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Hannah Lee", EmailAddress = "hannah.lee@example.com", PhoneNumber = "94939717", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Ian Walker", EmailAddress = "ian.walker@example.com", PhoneNumber = "82840896", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Julia Adams", EmailAddress = "julia.adams@example.com", PhoneNumber = "87361383", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Kyle Martin", EmailAddress = "kyle.martin@example.com", PhoneNumber = "58996963", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Laura Nelson", EmailAddress = "laura.nelson@example.com", PhoneNumber = "91113703", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Mike Scott", EmailAddress = "mike.scott@example.com", PhoneNumber = "87145172", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Nina Patel", EmailAddress = "nina.patel@example.com", PhoneNumber = "87361383", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Oscar Martinez", EmailAddress = "oscar.martinez@example.com", PhoneNumber = "83923344", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Pamela Anderson", EmailAddress = "pamela.anderson@example.com", PhoneNumber = "99498058", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Quincy Adams", EmailAddress = "quincy.adams@example.com", PhoneNumber = "99287448", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Rachel Green", EmailAddress = "rachel.green@example.com", PhoneNumber = "87542242", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Sam Wilson", EmailAddress = "sam.wilson@example.com", PhoneNumber = "85980281", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Tina Brown", EmailAddress = "tina.brown@example.com", PhoneNumber = "91353642", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Ursula Grant", EmailAddress = "ursula.grant@example.com", PhoneNumber = "97145172", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Victor Lee", EmailAddress = "victor.lee@example.com", PhoneNumber = "95980281", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Wendy Hill", EmailAddress = "wendy.hill@example.com", PhoneNumber = "86366852", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Xander Cole", EmailAddress = "xander.cole@example.com", PhoneNumber = "87361383", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Yvonne Moore", EmailAddress = "yvonne.moore@example.com", PhoneNumber = "91113703", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Zachary Hughes", EmailAddress = "zachary.hughes@example.com", PhoneNumber = "91353642", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Amy Turner", EmailAddress = "amy.turner@example.com", PhoneNumber = "91113703", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "Ben Lewis", EmailAddress = "ben.lewis@example.com", PhoneNumber = "85745072", Gender = Domain.Enums.GenderType.Male },
            new Employee { Name = "Clara Scott", EmailAddress = "clara.scott@example.com", PhoneNumber = "81353642", Gender = Domain.Enums.GenderType.Female },
            new Employee { Name = "David King", EmailAddress = "david.king@example.com", PhoneNumber = "95980281", Gender = Domain.Enums.GenderType.Male}
        ];
        private readonly IApplicationDbContext _context = context;
        private readonly IEmployeeIdProvider _idProvider = idProvider;

        public async Task SeedAsync()
        {
            Random random = new();
            if (!await _context.Employees.AnyAsync())
            {
                List<Cafe> cafes = await _context.Cafes.ToListAsync();

                foreach (var emp in employees)
                {
                    // Get a random index
                    int randomIndex = random.Next(cafes.Count);
                    Cafe randomCafe = cafes[randomIndex];
                    Guid tempCafeId = randomCafe.Id;

                    int randomDays = random.Next(maxValue: 10);
                    emp.CafeId = tempCafeId;
                    emp.StartedAt = DateTime.Now.AddDays(-randomDays);

                    emp.Id = await _idProvider.NextIdAsync();
                    await _context.Employees.AddAsync(emp);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}