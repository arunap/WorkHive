using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using WorkHive.Application.Abstraction;
using WorkHive.Infrastructure.Database;

namespace WorkHive.UnitTests
{
    public static class MockExtesions
    {
        public static ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"InMemoryDatabase_{Guid.NewGuid()}")
                .Options;

            var _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            var _mediator = new Mock<IMediator>();

            return new ApplicationDbContext(options, _mediator.Object);
        }
    }
}