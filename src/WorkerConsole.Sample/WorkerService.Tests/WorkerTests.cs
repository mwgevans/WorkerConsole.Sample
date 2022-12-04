using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
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

            /*
             *  loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Index page say hello", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
             */

            // Act


            // Assert
            Assert.NotNull(worker);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Run_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int expectedCount = 5;
            int count = 0;
            var worker = this.CreateWorker();
            CancellationTokenSource cts = new CancellationTokenSource();
            //CancellationToken stoppingToken = default(global::System.Threading.CancellationToken);
            mockLogger.Setup(l => l.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>())).Callback(() => {
                    count++;
                    if (count == expectedCount)
                        cts.Cancel();
                });

            // Act
            var result = await Record.ExceptionAsync(async () => await worker.Run(cts.Token));

            // Assert
            Assert.IsType<TaskCanceledException>(result);
            this.mockRepository.VerifyAll();
        }

    }
}
