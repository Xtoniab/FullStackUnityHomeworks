using System;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test]
        public void Converter_ShouldThrowException_WhenResourcesPerCycleIsZero()
        {
            // Arrange
            var loadingArea = new ResourceArea(5);
            var unloadingArea = new ResourceArea(5);

            // Act
            Action action = () => new ResourceConverter(
                loadingArea,
                unloadingArea,
                0,
                1,
                5
            );

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*positive*");
        }
    }
}