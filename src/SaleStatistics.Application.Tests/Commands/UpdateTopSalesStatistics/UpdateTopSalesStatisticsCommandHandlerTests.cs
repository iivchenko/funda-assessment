using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SaleStatistics.Application.Commands.UpdateTopSalesStatistics;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Tests.Commands.UpdateTopSalesStatistics
{
    [TestFixture]
    public sealed class UpdateTopSalesStatisticsCommandHandlerTests
    {
        private UpdateTopSalesStatisticsCommandHandler _handler;

        private Mock<ISaleService> _saleServiceMock;
        private Mock<ISaleStatisticRepository> _saleStatisticRepositoryMock;
        private Mock<ILogger<UpdateTopSalesStatisticsCommandHandler>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _saleServiceMock = new Mock<ISaleService>();
            _saleStatisticRepositoryMock = new Mock<ISaleStatisticRepository>();
            _loggerMock = new Mock<ILogger<UpdateTopSalesStatisticsCommandHandler>>();

            _handler = new UpdateTopSalesStatisticsCommandHandler(
                _saleServiceMock.Object, 
                _saleStatisticRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Test]
        public async Task Handle_NoStatisticsInRepository_DoNothing()
        {
            // Arrange
            _saleStatisticRepositoryMock
                .Setup(x => x.GetSaleStatistics())
                .ReturnsAsync(Enumerable.Empty<SalesStatistic>());

            var command = new UpdateTopSalesStatisticsCommand();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _saleStatisticRepositoryMock.Verify(x => x.GetSaleStatistics(), Times.Once);
            _saleStatisticRepositoryMock.Verify(x => x.UpdateSaleStatistics(It.IsAny<SalesStatistic>()), Times.Never);
        }

        [Test]
        public async Task Handle_NoSalesForSpecifiedStatistics_SaveEmptyStatistics()
        {
            // Arrange
            var statistic = new SalesStatistic
            {
                Id = Guid.NewGuid(),
                
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Descripiton = "Test",
                Criteria = new SaleStatisticCriteria
                {
                    Count = 1,
                    Filter = "filter"
                },
                Items = new List<SaleStatisticItem>
                {
                    new SaleStatisticItem()
                }
            };

            _saleServiceMock
                .Setup(x => x.ReadSales(statistic.Criteria.Filter))
                .ReturnsAsync(Enumerable.Empty<Sale>());

            _saleStatisticRepositoryMock
                .Setup(x => x.GetSaleStatistics())
                .ReturnsAsync(new[] { statistic });

            var command = new UpdateTopSalesStatisticsCommand();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _saleStatisticRepositoryMock.Verify(x => x.GetSaleStatistics(), Times.Once);
            _saleStatisticRepositoryMock.Verify(x => x.UpdateSaleStatistics(statistic), Times.Once);

            Assert.That(statistic.Items.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_HappyPath_SaveStatistics()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale(Guid.NewGuid(), 1, "First agent"),
                new Sale(Guid.NewGuid(), 2, "Second agent"),
                new Sale(Guid.NewGuid(), 3, "Third agent"),
                new Sale(Guid.NewGuid(), 1, "First agent"),
                new Sale(Guid.NewGuid(), 1, "First agent"),
                new Sale(Guid.NewGuid(), 2, "Second agent"),
                new Sale(Guid.NewGuid(), 1, "First agent")
            };

            var statistic = new SalesStatistic
            {
                Id = Guid.NewGuid(),

                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                Descripiton = "Test",
                Criteria = new SaleStatisticCriteria
                {
                    Count = 2,
                    Filter = "filter"
                },
                Items = new List<SaleStatisticItem>()
            };

            _saleServiceMock
                .Setup(x => x.ReadSales(statistic.Criteria.Filter))
                .ReturnsAsync(sales);

            _saleStatisticRepositoryMock
               .Setup(x => x.GetSaleStatistics())
               .ReturnsAsync(new[] { statistic });

            var command = new UpdateTopSalesStatisticsCommand();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _saleStatisticRepositoryMock.Verify(x => x.GetSaleStatistics(), Times.Once);
            _saleStatisticRepositoryMock.Verify(x => x.UpdateSaleStatistics(statistic), Times.Once);

            Assert.That(statistic.Items.Count(), Is.EqualTo(2), "Top two statistics should be created");
            Assert.That(statistic.Items.First().RealEstateAgent, Is.EqualTo("First agent"), "'First Agent' should in the top");
            Assert.That(statistic.Items.First().SalesCount, Is.EqualTo(4), "Incorect sales count for 'First Agent'");
            Assert.That(statistic.Items.ElementAt(1).RealEstateAgent, Is.EqualTo("Second agent"), "'Second Agent' should in the second position of the top");
            Assert.That(statistic.Items.ElementAt(1).SalesCount, Is.EqualTo(2), "Incorect sales count for 'Second Agent'");
        }
    }
}
