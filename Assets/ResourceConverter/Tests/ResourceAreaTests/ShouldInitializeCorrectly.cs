using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 3)]
        [TestCase(10, 7)]
        public void ResourceArea_ShouldInitializeCorrectly(int capacity, int initialResources)
        {
            // Arrange
            var resourceArea = new ResourceArea(capacity, initialResources);
            
            // Assert
            resourceArea.Capacity.Should().Be(capacity);
            resourceArea.ResourceCount.Should().Be(initialResources);
        }

    }
}