using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 7, 5, 2)]
        [TestCase(10, 15, 10, 5)]
        [TestCase(8, 8, 8, 0)]
        [TestCase(5, 3, 3, 0)]
        public void LoadingArea_ShouldGiveBackExcessResources_WhenCapacityExceeded(
            int capacity,
            int amountToAdd,
            int expectedResourceCount,
            int expectedExcess)
        {
            // Arrange
            var loadingArea = new ResourceArea(capacity);

            // Act
            var excess = loadingArea.AddResources(amountToAdd);

            // Assert
            loadingArea.ResourceCount.Should().Be(expectedResourceCount);
            excess.Should().Be(expectedExcess);
        }
    }
}