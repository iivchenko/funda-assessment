using NUnit.Framework;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Tests.Repositories
{
    [TestFixture]
    public sealed class InMemorySaleStatisticRepositoryTests
    {
        private InMemorySaleStatisticRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new InMemorySaleStatisticRepository();
        }

        [Test]
        public async Task GetSaleStatistics_RepositoryEmpty_ReturnEmpty()
        {
            // Act
            var statistics = await _repository.GetSaleStatistics();

            // Assert

            Assert.That(statistics, Is.Empty, "Repository should be empty");
        }

        [Test]
        public async Task GetSaleStatistics_UpdateSaleStatistics_RepositoryNotEmpty_ReturnStatistics()
        {
            // Arrange
            var id = Guid.NewGuid();

            var statistic = new SalesStatistic
            {
                Id = id
            };

            await _repository.UpdateSaleStatistics(statistic);

            // Act
            var statistics = await _repository.GetSaleStatistics();

            // Assert
            Assert.That(statistics.Count(), Is.EqualTo(1), "Only one statistics should exist in repository");
            Assert.That(statistics.First().Id, Is.EqualTo(id), "Wrong statistic persited");
        }

        [Test]
        public async Task UpdateSaleStatistics_RepositoryHasTheStatistic_UpdateExitingStatistic()
        {
            // Arrange
            var id = Guid.NewGuid();

            var statistic = new SalesStatistic
            {
                Id = id,
                Descripiton = "Initial"
            };

            await _repository.UpdateSaleStatistics(statistic);

            // Act
            var expectedEtatistic = new SalesStatistic
            {
                Id = id,
                Descripiton = "Expected"
            };

            await _repository.UpdateSaleStatistics(expectedEtatistic);

            // Assert
            var statistics = await _repository.GetSaleStatistics();

            Assert.That(statistics.Count(), Is.EqualTo(1), "Only one updated statistics should exist in repository");
            Assert.That(statistics.First().Id, Is.EqualTo(id), "Wrong statistic persited");
            Assert.That(statistics.First().Descripiton, Is.EqualTo("Expected"), "Wrong statistic persited");
        }
    }
}
