using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;

namespace WorkHive.Infrastructure.Database.Seeds
{
    public class CafeDataInitializer(IApplicationDbContext context)
    {
        private static readonly List<Cafe> cafes =
        [
            new Cafe { Id = Guid.NewGuid(), Name = "JavaHub", Description = "A cozy cafe with a variety of coffee blends.", Location = "123 Coffee St, Seattle, WA", Logo = new Domain.FileInfo.FileStore{ FileName="5309c5c1-dd2a-4905-a416-c958206fa488.png", FilePath="Uploads\\Seeds\\5309c5c1-dd2a-4905-a416-c958206fa488.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "BrewLab", Description = "Perfect spot for coffee enthusiasts.", Location = "456 Brew Ave, Portland, OR" , Logo = new Domain.FileInfo.FileStore{ FileName="d8836edb-88fa-4201-8ea9-fb468f962909.png", FilePath="Uploads\\Seeds\\d8836edb-88fa-4201-8ea9-fb468f962909.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "GrindIt", Description = "Trendy cafe with organic options.", Location = "789 Bean Blvd, Austin, TX" , Logo = new Domain.FileInfo.FileStore{ FileName="a5df36d1-ff4c-407f-ab54-d529b2d50624.png", FilePath="Uploads\\Seeds\\a5df36d1-ff4c-407f-ab54-d529b2d50624.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "RoastUp", Description = "Specialty coffee and fresh pastries.", Location = "101 Espresso Rd, San Francisco, CA", Logo = new Domain.FileInfo.FileStore{ FileName="5309c5c1-dd2a-4905-a416-c958206fa488.png", FilePath="Uploads\\Seeds\\5309c5c1-dd2a-4905-a416-c958206fa488.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "CafeZen", Description = "A peaceful spot for coffee and tea lovers.", Location = "202 Calm St, Denver, CO" , Logo = new Domain.FileInfo.FileStore{ FileName="dc63f20b-bb2f-4233-a6ec-177b4c7ba835.png", FilePath="Uploads\\Seeds\\dc63f20b-bb2f-4233-a6ec-177b4c7ba835.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "PerkPal", Description = "Casual cafe for a quick coffee fix.", Location = "303 Java Ln, Miami, FL" , Logo = new Domain.FileInfo.FileStore{ FileName="f34b749d-c028-49a6-b9d5-e3302502f7fb.png", FilePath="Uploads\\Seeds\\f34b749d-c028-49a6-b9d5-e3302502f7fb.png", ContentType="image"} },
            new Cafe { Id = Guid.NewGuid(), Name = "SipCity", Description = "Modern cafe with a variety of drinks.", Location = "404 Brewster St, New York, NY" , Logo = new Domain.FileInfo.FileStore{ FileName="f5dca0b4-99cd-468c-88a2-a60e88c531b9.png", FilePath="Uploads\\Seeds\\f5dca0b4-99cd-468c-88a2-a60e88c531b9.png", ContentType="image"} }
        ];

        private readonly IApplicationDbContext _context = context;

        public async Task SeedAsync()
        {
            if (!await _context.Cafes.AnyAsync())
            {
                await _context.Cafes.AddRangeAsync(cafes);
                await _context.SaveChangesAsync();
            }
        }
    }
}