using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 0)]
        [TestCase(10, 5)]
        [TestCase(3, 3)]
        public void ResourceArea_ShouldHaveInitialResources_WhenInitialized(int capacity, int initialResources)
        {
            // Arrange
            var resourceArea = new ResourceArea(capacity, initialResources);

            // Assert
            resourceArea.ResourceCount.Should().Be(initialResources);
        }
    }
}