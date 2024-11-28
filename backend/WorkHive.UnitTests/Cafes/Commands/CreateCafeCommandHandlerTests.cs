using Microsoft.AspNetCore.Http;
using Moq;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Cafes.Commands.Create;

namespace WorkHive.UnitTests.Cafes.Commands
{
    public class CreateCafeCommandHandlerTests
    {
        private readonly IApplicationDbContext _context;
        private readonly Mock<IImageUploader> _mockImageUploader;
        private readonly CreateCafeCommandHandler _handler;

        public CreateCafeCommandHandlerTests()
        {
            _context = MockExtesions.GetContext();

            _mockImageUploader = new Mock<IImageUploader>();
            _handler = new CreateCafeCommandHandler(_context, _mockImageUploader.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateCafeWithLogo_WhenLogoIsProvided()
        {
            var logoId = Guid.NewGuid();
            // Arrange
            var command = new CreateCafeCommand
            {
                Name = "Test Cafe",
                Description = "A cozy place to relax.",
                Logo = new Mock<IFormFile>().Object,
                Location = "Test Location"
            };

            _mockImageUploader.Setup(x => x.UploadFileAsync(command.Logo)).ReturnsAsync(logoId);

            // Act
            var insertedCafeId = await _handler.Handle(command, CancellationToken.None);
            var insertedCafe = _context.Cafes.FirstOrDefault(x => x.Id == insertedCafeId);

            Assert.NotNull(insertedCafe);
            Assert.NotNull(insertedCafe.LogoId);
            Assert.NotEqual(Guid.Empty, insertedCafeId); // Ensure a valid GUID is returned
        }

        [Fact]
        public async Task Handle_ShouldCreateCafeWithoutLogo_WhenLogoIsNotProvided()
        {
            // Arrange
            var command = new CreateCafeCommand
            {
                Name = "Test Cafe",
                Description = "A cozy place to relax.",
                Logo = null,
                Location = "Test Location"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var insertedCafe = _context.Cafes.FirstOrDefault(x => x.Id == result);

            // Assert
            var uploadedImg = _context.FileStores.FirstOrDefault(x => x.Id == insertedCafe.LogoId);
            Assert.Null(uploadedImg);
            Assert.NotEqual(Guid.Empty, result); // Ensure a valid GUID is returned
        }
    }

}