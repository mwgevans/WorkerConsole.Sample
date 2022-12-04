using Microsoft.Extensions.Logging;
using Moq;
using System;
using WorkerService;
using Xunit;

namespace WorkerService.Tests
{
    public class WorkerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<Worker>> mockLogger;

        public WorkerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<Worker>>();
        }

        private Worker CreateWorker()
        {
            return new Worker(
                this.mockLogger.Object);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var worker = this.CreateWorker();

            // Act


            // Assert
            Assert.NotNull(worker);
            this.mockRepository.VerifyAll();
        }
    }
}
