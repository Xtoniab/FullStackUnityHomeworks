using System;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(5, 6)]
        [TestCase(0, 1)]
        [TestCase(10, 100)]
        public void ResourceArea_ShouldThrowException_WhenInitialCountExceedsCapacity(int capacity, int initialResources)  
        {
            // Act
            Action action = () => new ResourceArea(capacity, initialResources);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*exceed capacity*");
        }

    }
}