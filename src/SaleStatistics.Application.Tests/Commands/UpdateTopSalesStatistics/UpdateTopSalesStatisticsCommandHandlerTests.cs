using Moq;
using NUnit.Framework;
using SaleStatistics.Application.Commands.UpdateTopSalesStatistics;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Tests.Commands.UpdateTopSalesStatistics
{
    [TestFixture]
    public sealed class UpdateTopSalesStatisticsCommandHandlerTests
    {
        private UpdateTopSalesStatisticsCommandHandler _handler;

        private Mock<ISaleService> _saleServiceMock;
        private Mock<ISaleStatisticRepository> _saleStatisticRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _saleServiceMock = new Mock<ISaleService>();
            _saleStatisticRepositoryMock = new Mock<ISaleStatisticRepository>();

            _handler = new UpdateTopSalesStatisticsCommandHandler(_saleServiceMock.Object, _saleStatisticRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_NoSalesForSpecifiedStatistics_SaveEmptyStatistics()
        {
            // Arrange           
            // Act           
            // Assert
        }

        [Test]
        public async Task Handle_HappyPath_SaveStatistics()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
