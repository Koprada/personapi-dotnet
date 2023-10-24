using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TechnicalTestMasiv.Controllers;

namespace TechnicalTestMasiv.Tests
{
    public class ElevatorControllerTests
    {
        private readonly Mock<IDataRepository> _mockDataRepository;
        private readonly ElevatorController _controller;

        public ElevatorControllerTests()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _controller = new ElevatorController(_mockDataRepository.Object);
        }

        [Fact]
        public async Task MoveElevator_ShouldAddDataAndReturnOk()
        {
            // Arrange
            int targetFloor = 5;
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Data>()))
                .Returns(Task.FromResult(new Data()));  // Change made here

            // Act
            var result = await _controller.MoveElevator(targetFloor);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockDataRepository.Verify(repo => repo.AddAsync(It.Is<Data>(data => 
                data.Floor == targetFloor && data.Type == "move")), Times.Once);
        }

        [Fact]
        public async Task CallElevator_ShouldAddDataAndReturnOk()
        {
            // Arrange
            int calledFloor = 3;
            _mockDataRepository.Setup(repo => repo.AddAsync(It.IsAny<Data>()))
                .Returns(Task.FromResult(new Data()));

            // Act
            var result = await _controller.CallElevator(calledFloor);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockDataRepository.Verify(repo => repo.AddAsync(It.Is<Data>(data => 
                data.Floor == calledFloor && data.Type == "call")), Times.Once);
        }

        [Fact]
        public async Task StartElevator_WhenElevatorIsMoving_ShouldReturnBadRequest()
        {
            // Arrange
            _mockDataRepository.Setup(repo => repo.GetElevatorStatusAsync())
                .Returns(Task.FromResult("Moving"));
            _mockDataRepository.Setup(repo => repo.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<TechnicalTestMasiv.Data>>(new List<TechnicalTestMasiv.Data> { new Data() })); // Change made here

            // Act
            var result = await _controller.StartElevator();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task StartElevator_WhenElevatorIsNotMoving_ShouldReturnOk()
        {
            // Arrange
            _mockDataRepository.Setup(repo => repo.GetElevatorStatusAsync())
                .Returns(Task.FromResult("Stopped"));
            _mockDataRepository.Setup(repo => repo.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<TechnicalTestMasiv.Data>>(new List<TechnicalTestMasiv.Data>())); // Change made here
            _mockDataRepository.Setup(repo => repo.SetElevatorStatusAsync("Moving"))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.StartElevator();

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
