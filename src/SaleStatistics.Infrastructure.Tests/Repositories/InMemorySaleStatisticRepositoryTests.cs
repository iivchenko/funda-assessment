using NUnit.Framework;
using SaleStatistics.Infrastructure.Repositories;

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
        public void GetSaleStatistics_RepositoryEmpty_ReturnEmpty()
        {
            // Arrange
            // Act
            // Assert
        }

        [Test]
        public void GetSaleStatistics_RepositoryNotEmpty_ReturnStatistics()
        {
            // Arrange
            // Act
            // Assert
        }

        [Test]
        public void UpdateSaleStatistics_RepositoryEmpty_CreatesNewStatistics()
        {
            // Arrange
            // Act
            // Assert
        }

        [Test]
        public void UpdateSaleStatistics_RepositoryHasTheStatistic_UpdateExitingStatistic()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
