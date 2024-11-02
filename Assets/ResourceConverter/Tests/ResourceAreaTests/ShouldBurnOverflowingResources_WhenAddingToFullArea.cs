using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 5, 3, 5, 3)]
        [TestCase(5, 0, 3, 3, 0)]
        [TestCase(5, 4, 2, 5, 1)]
        public void LoadingArea_ShouldBurnOverflowingResources_WhenAddingToFullArea(
            int capacity,
            int initialAdd,
            int amountToAdd,
            int expectedResourceCount,
            int expectedBurned)
        {
            // Arrange
            var loadingArea = new ResourceArea(capacity, initialAdd);

            // Act
            var burned = loadingArea.AddResources(amountToAdd);

            // Assert
            loadingArea.ResourceCount.Should().Be(expectedResourceCount);
            burned.Should().Be(expectedBurned);
        }
    }
}