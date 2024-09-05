using BikeManagement.Controllers;
using BikeManagement.Dtos;
using BikeManagement.Interfaces.UnitOfWork;
using BikeManagement.Models;
using BikeManagement.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using FluentValidation.Results;

namespace BikeManagement.Tests
{
    public class BikeControllerTests
    {
        private readonly BikeController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<BikeController>> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBikeValidationService> _mockValidationService;

        public BikeControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<BikeController>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidationService = new Mock<IBikeValidationService>();

            _controller = new BikeController(
                _mockMapper.Object,
                _mockLogger.Object,
                _mockUnitOfWork.Object,
                _mockValidationService.Object
            );
        }

        [Fact]
        public async Task CreateBike_ReturnsCreatedResult_WhenBikeIsValid()
        {
            // Arrange
            var bikeCreateDto = new BikeCreateDto { Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var bike = new Bike { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var bikeDto = new BikeDto { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };

            _mockValidationService.Setup(v => v.ValidateCreateAsync(bikeCreateDto))
                .ReturnsAsync(new ValidationResult { Errors = new List<ValidationFailure>() });

            _mockMapper.Setup(m => m.Map<Bike>(bikeCreateDto)).Returns(bike);
            _mockMapper.Setup(m => m.Map<BikeDto>(bike)).Returns(bikeDto);

            _mockUnitOfWork.Setup(u => u.Bikes.AddAsync(bike)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.CreateBike(bikeCreateDto) as CreatedAtActionResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(bikeDto);
        }

        [Fact]
        public async Task CreateBike_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var bikeCreateDto = new BikeCreateDto { Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("Brand", "Brand is required") };

            _mockValidationService.Setup(v => v.ValidateCreateAsync(bikeCreateDto))
                .ReturnsAsync(new ValidationResult { Errors = validationErrors });

            // Act
            var result = await _controller.CreateBike(bikeCreateDto) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            ((List<string>)result.Value).Should().Contain("Brand is required");
        }

        [Fact]
        public async Task GetBikes_ReturnsOkResult_WithListOfBikes()
        {
            // Arrange
            var bikes = new List<Bike>
            {
                new Bike { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" }
            };
            var bikeDtos = new List<BikeDto>
            {
                new BikeDto { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" }
            };

            _mockUnitOfWork.Setup(u => u.Bikes.GetAllAsync()).ReturnsAsync(bikes);
            _mockMapper.Setup(m => m.Map<List<BikeDto>>(bikes)).Returns(bikeDtos);

            // Act
            var result = await _controller.GetBikes() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(bikeDtos);
        }

        [Fact]
        public async Task GetBike_ReturnsOkResult_WhenBikeExists()
        {
            // Arrange
            var bike = new Bike { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var bikeDto = new BikeDto { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };

            _mockUnitOfWork.Setup(u => u.Bikes.GetByIdAsync(1)).ReturnsAsync(bike);
            _mockMapper.Setup(m => m.Map<BikeDto>(bike)).Returns(bikeDto);

            // Act
            var result = await _controller.GetBike(1) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(bikeDto);
        }

        [Fact]
        public async Task GetBike_ReturnsNotFound_WhenBikeDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Bikes.GetByIdAsync(1)).ReturnsAsync((Bike)null);

            // Act
            var result = await _controller.GetBike(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateBike_ReturnsOkResult_WhenBikeIsUpdated()
        {
            // Arrange
            var bikeUpdateDto = new BikeUpdateDto { Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var bike = new Bike { Id = 1, Brand = "Old Brand", Model = "Old Model", Year = 2021, OwnerEmail = "oldowner@example.com" };
            var updatedBike = new Bike { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };
            var bikeDto = new BikeDto { Id = 1, Brand = "Yamaha", Model = "YZF-R3", Year = 2022, OwnerEmail = "owner@example.com" };

            _mockValidationService.Setup(v => v.ValidateUpdateAsync(bikeUpdateDto))
                .ReturnsAsync(new ValidationResult { Errors = new List<ValidationFailure>() }); // No errors
            _mockUnitOfWork.Setup(u => u.Bikes.GetByIdAsync(1)).ReturnsAsync(bike);
            _mockMapper.Setup(m => m.Map(bikeUpdateDto, bike)).Returns(updatedBike);
            _mockMapper.Setup(m => m.Map<BikeDto>(updatedBike)).Returns(bikeDto);

            _mockUnitOfWork.Setup(u => u.Bikes.Update(updatedBike));
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.UpdateBike(1, bikeUpdateDto) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(bikeDto);
        }

        [Fact]
        public async Task DeleteBike_ReturnsOkResult_WhenBikeIsDeleted()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Bikes.DeleteAsync(1)).ReturnsAsync(true);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteBike(1) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.ToString().Should().Contain("Bike deleted successfully.");
        }

        [Fact]
        public async Task DeleteBike_ReturnsNotFound_WhenBikeDoesNotExist()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Bikes.DeleteAsync(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteBike(1) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
        }
    }
}
