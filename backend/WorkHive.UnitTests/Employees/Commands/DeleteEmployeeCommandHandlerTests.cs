using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Employees.Commands.Delete;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Exceptions;
using WorkHive.Domain.Shared.Enums;

namespace WorkHive.UnitTests.Employees.Commands
{
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly DeleteEmployeeCommandHandler _handler;
        private readonly IApplicationDbContext _context;
        public DeleteEmployeeCommandHandlerTests()
        {
            _context = MockExtesions.GetContext();
            _handler = new DeleteEmployeeCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_ShouldDeleteEmployee_WhenEmployeeExists()
        {
            // Arrange
            var existingEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "1234567890",
                Gender = GenderType.Male,
                CafeId = Guid.NewGuid(),
                StartedAt = DateTime.UtcNow.AddYears(-1)
            };

            _context.Employees.Add(existingEmployee);
            await _context.SaveChangesAsync();


            // Act
            await _handler.Handle(new DeleteEmployeeCommand { Id = existingEmployee.Id }, CancellationToken.None);

            // Assert
            Assert.Null(_context.Employees.FirstOrDefault(e => e.Id == existingEmployee.Id));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();

            // Arrange
            var existingEmployee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "1234567890",
                Gender = GenderType.Male,
                CafeId = Guid.NewGuid(),
                StartedAt = DateTime.UtcNow.AddYears(-1)
            };

            _context.Employees.Add(existingEmployee);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => _handler.Handle(new DeleteEmployeeCommand { Id = employeeId }, CancellationToken.None));
        }
    }
}