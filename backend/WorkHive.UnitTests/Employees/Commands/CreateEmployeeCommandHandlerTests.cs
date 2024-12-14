using Moq;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Employees.Commands.Create;
using WorkHive.Domain.Shared.Enums;

namespace WorkHive.UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly IApplicationDbContext _context;
        private readonly Mock<IEmployeeIdProvider> _mockEmployeeIdProvider;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _context = MockExtesions.GetContext();

            var _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockEmployeeIdProvider = new Mock<IEmployeeIdProvider>();

            var currentUtcTime = DateTime.UtcNow;
            _mockDateTimeProvider.Setup(x => x.UtcNow).Returns(currentUtcTime);

            _handler = new CreateEmployeeCommandHandler(_context, _mockDateTimeProvider.Object, _mockEmployeeIdProvider.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateEmployeeAndReturnId()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                EmailAddress = "test@example.com",
                PhoneNumber = "1234567890",
                Gender = GenderType.Male,
                Name = "John Doe",
                CafeId = Guid.NewGuid()
            };

            var expectedId = Guid.NewGuid().ToString();
            _mockEmployeeIdProvider.Setup(x => x.NextIdAsync()).ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Fact]
        public async Task Handle_ShouldSetCafeIdToNullIfCafeIdIsEmpty()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                EmailAddress = "test@example.com",
                PhoneNumber = "1234567890",
                Gender = GenderType.Male,
                Name = "John Doe",
                CafeId = Guid.Empty // CafeId is empty
            };

            var expectedId = Guid.NewGuid().ToString();
            _mockEmployeeIdProvider.Setup(x => x.NextIdAsync()).ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, result);
            Assert.Null(_context.Employees.First(e => e.Id == expectedId).CafeId);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionIfEmployeeIdProviderFails()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                EmailAddress = "test@example.com",
                PhoneNumber = "1234567890",
                Gender = GenderType.Male,
                Name = "John Doe",
                CafeId = Guid.NewGuid()
            };

            _mockEmployeeIdProvider.Setup(x => x.NextIdAsync()).ThrowsAsync(new Exception("Error generating ID"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}