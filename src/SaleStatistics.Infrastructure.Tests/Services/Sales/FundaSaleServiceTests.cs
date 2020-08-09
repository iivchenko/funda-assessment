
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SaleStatistics.Application.Services.Sales;
using SaleStatistics.Infrastructure.Services.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Tests.Services.Sales
{
    [TestFixture]
    public sealed class FundaSaleServiceTests
    {
        [Test]
        public async Task ReadSales_OneRequest_CorrectSalesReturned()
        {
            // Arrange
            var itemsPerPage = 10;
            var maxPages = 15;
            var maxRequestsPerMinute = 10;

            var settings = new FundaSettings
            {
                ApiAddress = string.Empty,
                CacheExpiration = TimeSpan.FromMinutes(10),
                Key = Guid.NewGuid(),
                PageSize = 10,
                RequestDelay = TimeSpan.FromMilliseconds(300)
            };

            var client = new FundaClientFake(itemsPerPage, maxPages, maxRequestsPerMinute);
            var service = CreateService(settings, client);

            // Act
            var sales = await service.ReadSales("1");

            // Assert
            Assert.That(sales.Count(), Is.EqualTo(itemsPerPage * maxPages));
        }

        [Test]
        public async Task ReadSales_SeveralRequestsWithSameFilter_CorrectSalesReturnedAndSalesPulledOnce()
        {
            // Arrange
            var itemsPerPage = 10;
            var maxPages = 15;
            var maxRequestsPerMinute = 10;

            var settings = new FundaSettings
            {
                ApiAddress = string.Empty,
                CacheExpiration = TimeSpan.FromMinutes(10),
                Key = Guid.NewGuid(),
                PageSize = 10,
                RequestDelay = TimeSpan.FromMilliseconds(300)
            };

            var client = new FundaClientFake(itemsPerPage, maxPages, maxRequestsPerMinute);
            var service = CreateService(settings, client);

            // Act
            var salesT1 = service.ReadSales("1");
            var salesT2 = service.ReadSales("1");
            var salesT3 = service.ReadSales("1");
            var salesT4 = service.ReadSales("1");

            var sales = await Task.WhenAll(salesT1, salesT2, salesT3, salesT4);

            // Assert
            Assert.That(sales[0].Count(), Is.EqualTo(150));
            Assert.That(sales[1].Count(), Is.EqualTo(150));
            Assert.That(sales[2].Count(), Is.EqualTo(150));
            Assert.That(sales[3].Count(), Is.EqualTo(150));

            Assert.That(client.GetObjectsMethodCallCount, Is.EqualTo(maxPages + 1));
        }

        [Test]
        public async Task ReadSales_SeveralRequestsWithDifferentFilters_CorrectSalesReturnedAndSalesPulledThreeTimes()
        {
            // Arrange
            var itemsPerPage = 10;
            var maxPages = 15;
            var maxRequestsPerMinute = 10;

            var settings = new FundaSettings
            {
                ApiAddress = string.Empty,
                CacheExpiration = TimeSpan.FromMinutes(10),
                Key = Guid.NewGuid(),
                PageSize = 10,
                RequestDelay = TimeSpan.FromMilliseconds(300)
            };

            var client = new FundaClientFake(itemsPerPage, maxPages, maxRequestsPerMinute);
            var service = CreateService(settings, client);

            // Act
            var salesT1 = service.ReadSales("1");
            var salesT2 = service.ReadSales("2");
            var salesT3 = service.ReadSales("3");     

            var sales = await Task.WhenAll(salesT1, salesT2, salesT3);

            // Assert
            Assert.That(sales[0].Count(), Is.EqualTo(150));
            Assert.That(sales[1].Count(), Is.EqualTo(150));
            Assert.That(sales[2].Count(), Is.EqualTo(150));

            Assert.That(client.GetObjectsMethodCallCount, Is.EqualTo((maxPages + 1) * 3));
        }

        [Test]
        public void ReadSales_IncorectFundaSettingsExceedsNumberOfAllowedRequests_Throws()
        {
            // Arrange
            var itemsPerPage = 10;
            var maxPages = 15;
            var maxRequestsPerMinute = 5;

            var settings = new FundaSettings
            {
                ApiAddress = string.Empty,
                CacheExpiration = TimeSpan.FromMinutes(10),
                Key = Guid.NewGuid(),
                PageSize = 10,
                RequestDelay = TimeSpan.FromMilliseconds(30)
            };

            var client = new FundaClientFake(itemsPerPage, maxPages, maxRequestsPerMinute);
            var service = CreateService(settings, client);

            // Act + Assert
            Assert.That(
                async () => await service.ReadSales("1"),
                Throws.InstanceOf<Exception>().With.Message.EqualTo(FundaClientFake.NumberOfRequestsExceeded));
        }

        private FundaSaleService CreateService(FundaSettings settings, FundaClientFake client)
        {
            var options = new Mock<IOptions<FundaSettings>>();

            options = new Mock<IOptions<FundaSettings>>();

            options.SetupGet(x => x.Value).Returns(settings);

            var factory = new Mock<IFundaClientFactory>();
            factory.Setup(x => x.Create()).Returns(client);

            var mapper = new Mock<IMapper>();

            mapper
                .Setup(x => x.Map<IEnumerable<FundaObject>, IEnumerable<Sale>>(It.IsAny<IEnumerable<FundaObject>>()))
                .Returns<IEnumerable<FundaObject>>(x => Enumerable.Repeat(new Sale(Guid.NewGuid(), 0, null), x.Count()));

            return new FundaSaleService(options.Object, factory.Object, mapper.Object);
        }
    }
}
