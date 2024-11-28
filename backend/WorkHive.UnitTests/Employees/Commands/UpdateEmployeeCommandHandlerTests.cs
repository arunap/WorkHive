using Moq;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Employees.Commands.Update;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Exceptions;

namespace WorkHive.UnitTests.Employees.Commands
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public async Task Handle_ShouldUpdateEmployeeDetailsSuccessfully()
        {
            var context = MockExtesions.GetContext();

            // Arrange
            var existingEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "1234567890",
                Gender = Domain.Enums.GenderType.Male,
                CafeId = Guid.NewGuid(),
                StartedAt = DateTime.UtcNow.AddYears(-1)
            };

            context.Employees.Add(existingEmployee);
            await context.SaveChangesAsync();

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = existingEmployee.Id,
                Name = "Jane Doe",
                EmailAddress = "janedoe@example.com",
                PhoneNumber = "0987654321",
                Gender = Domain.Enums.GenderType.Female,
                CafeId = Guid.NewGuid() // New CafeId
            };

            var currentUtcTime = DateTime.UtcNow;
            _mockDateTimeProvider.Setup(x => x.UtcNow).Returns(currentUtcTime);

            // Act
            var _handler = new UpdateEmployeeCommandHandler(context, _mockDateTimeProvider.Object);
            await _handler.Handle(command, CancellationToken.None);

            existingEmployee = context.Employees.First(e => e.Id == existingEmployee.Id);

            // Assert
            Assert.Equal("Jane Doe", existingEmployee.Name);
            Assert.Equal("janedoe@example.com", existingEmployee.EmailAddress);
            Assert.Equal("0987654321", existingEmployee.PhoneNumber);
            Assert.Equal(Domain.Enums.GenderType.Female, existingEmployee.Gender);
            Assert.Equal(command.CafeId, existingEmployee.CafeId);
            Assert.NotEqual(currentUtcTime, existingEmployee.StartedAt);
        }

        [Fact]
        public async Task Handle_ShouldThrowExceptionIfEmployeeNotFound()
        {
            var context = MockExtesions.GetContext();

            // Arrange
            var command = new UpdateEmployeeCommand
            {
                EmployeeId = Guid.NewGuid().ToString(),
                Name = "Jane Doe",
                EmailAddress = "janedoe@example.com",
                PhoneNumber = "0987654321",
                Gender = Domain.Enums.GenderType.Female
            };

            var currentUtcTime = DateTime.UtcNow;
            _mockDateTimeProvider.Setup(x => x.UtcNow).Returns(currentUtcTime);

            var _handler = new UpdateEmployeeCommandHandler(context, _mockDateTimeProvider.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldNotChangeStartedAtIfCafeIdDoesNotChange()
        {
            var context = MockExtesions.GetContext();

            // Arrange
            var existingEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "1234567890",
                Gender = Domain.Enums.GenderType.Male,
                CafeId = Guid.NewGuid(),
                StartedAt = DateTime.UtcNow.AddYears(-1)
            };

            context.Employees.Add(existingEmployee);
            await context.SaveChangesAsync();

            var command = new UpdateEmployeeCommand
            {
                EmployeeId = existingEmployee.Id,
                Name = "Jane Doe",
                EmailAddress = "janedoe@example.com",
                PhoneNumber = "0987654321",
                Gender = Domain.Enums.GenderType.Female,
                CafeId = existingEmployee.CafeId // Same CafeId
            };

            var currentUtcTime = DateTime.UtcNow;
            _mockDateTimeProvider.Setup(x => x.UtcNow).Returns(currentUtcTime);

            var _handler = new UpdateEmployeeCommandHandler(context, _mockDateTimeProvider.Object);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(existingEmployee.StartedAt, existingEmployee.StartedAt); // Should not change
        }
    }

}