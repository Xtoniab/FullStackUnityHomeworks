using System;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(-2)]
        [TestCase(-5)]
        [TestCase(-1)]
        public void ResourceArea_ShouldThrowException_WhenRemovingNegativeResources(int amountToRemove)
        {
            // Arrange
            var loadingArea = new ResourceArea(5, 3);

            // Act
            Action action = () => loadingArea.RemoveResources(amountToRemove);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*cannot be negative*");
        }

    }
}