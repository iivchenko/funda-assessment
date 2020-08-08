using Moq;
using NUnit.Framework;
using SaleStatistics.Application.Queries.GetTopSaleObjects;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Tests.Queries.GetTopSaleObjects
{
    [TestFixture]
    public sealed class GetTopSaleObjectsQueryHandlerTests
    {
        private GetTopSaleObjectsQueryHandler _handler;

        private Mock<ISaleService> _saleServiceMock;

        [SetUp]
        public void Setup()
        {
            _saleServiceMock = new Mock<ISaleService>();

            _handler = new GetTopSaleObjectsQueryHandler(_saleServiceMock.Object);
        }

        [Test]
        public async Task Handle_NoSalesBySpecifiedFilter_ReturnEmptyStatistics()
        {
            // Arrange
            var query = new GetTopSaleObjectsQuery
            {
                Count = 10,
                Filter = "/fake/"
            };

            _saleServiceMock
                .Setup(x => x.ReadSales(It.IsAny<string>()))
                .Returns(Enumerable.Empty<Sale>());

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(response.Statistics, Is.Empty, "No statistics should be returned");
        }

        [Test]
        public async Task Handle_CountIsZero_ReturnEmptyStatistics()
        {
            // Arrange
            var query = new GetTopSaleObjectsQuery
            {
                Count = 0,
                Filter = "/amsterdam/"
            };

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

            _saleServiceMock
                .Setup(x => x.ReadSales(It.IsAny<string>()))
                .Returns(sales);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(response.Statistics, Is.Empty, "No statistics should be returned");
        }

        [Test]
        public async Task Handle_HappyPath_ReturnCorrectStatistics()
        {
            // Arrange
            var query = new GetTopSaleObjectsQuery
            {
                Count = 2,
                Filter = "/amsterdam/"
            };

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

            _saleServiceMock
                .Setup(x => x.ReadSales(It.IsAny<string>()))
                .Returns(sales);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(response.Statistics, Has.Count.EqualTo(2), "Top two statistics should be created");
            Assert.That(response.Statistics.First().RealEstateAgent, Is.EqualTo("First agent"), "'First Agent' should in the top");
            Assert.That(response.Statistics.ElementAt(1).RealEstateAgent, Is.EqualTo("Second agent"), "'Second Agent' should in the second position of the top");
        }
    }
}
