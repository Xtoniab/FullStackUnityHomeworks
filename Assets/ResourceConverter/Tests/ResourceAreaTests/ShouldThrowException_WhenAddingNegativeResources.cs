using System;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceAreaTests
    {
        [TestCase(-3)]
        [TestCase(-1)]
        public void ResourceArea_ShouldThrowException_WhenAddingNegativeResources(int amountToAdd)
        {
            // Arrange
            var loadingArea = new ResourceArea(5);

            // Act
            Action action = () => loadingArea.AddResources(amountToAdd);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*cannot be negative*");
        }

    }
}