using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test]
        public void Converter_ShouldNotProcess_WhenTurnedOff()
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(new ConverterCreationData
            {
                LoadingCapacity = 5,
                UnloadingCapacity = 5,
                ResourcesTakenPerCycle = 2,
                ResourcesDeliveredPerCycle = 1,
                ConversionTimeSeconds = 5,
                InitialLoadingResources = 4,
            });

            // Act
            converter.Update(5);

            // Assert
            unloadingArea.ResourceCount.Should().Be(0);
            loadingArea.ResourceCount.Should().Be(4);
        }
    }
}