using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 3, 5, 3, 0)]
        [TestCase(5, 5, 2, 2, 3)]
        [TestCase(5, 0, 1, 0, 0)]
        public void ResourceArea_ShouldNotRemoveMoreThanAvailable(
            int capacity,
            int initialAdd,
            int amountToRemove,
            int expectedRemoved,
            int expectedRemaining)
        {
            // Arrange
            var loadingArea = new ResourceArea(capacity, initialAdd);

            // Act
            var removed = loadingArea.RemoveResources(amountToRemove);

            // Assert
            removed.Should().Be(expectedRemoved);
            loadingArea.ResourceCount.Should().Be(expectedRemaining);
        }
    }
}