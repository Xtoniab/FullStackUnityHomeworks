using System;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(-5)]
        [TestCase(-1)]
        public void ResourceArea_ShouldThrowException_WhenCreatedWithNegativeCapacity(int capacity)
        {
            // Act
            Action action = () => new ResourceArea(capacity);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*cannot be negative*");
        }

    }
}