using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterTurnedOffTestCases))]
        public void Converter_ShouldReturnResources_WhenTurnedOffDuringProcessing(
            ConverterCreationData converterData, ConverterTurnedOffTestData data)
        {
            // Arrange
            var (converter, loadingArea, _) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.DeltaTimeBeforeTurnOff);
            converter.TurnOff();

            // Assert
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResourcesAfterTurnOff);
        }

        public static IEnumerable<TestCaseData> ConverterTurnedOffTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 5,
                    UnloadingCapacity = 5,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 5,
                },
                new ConverterTurnedOffTestData
                {
                    DeltaTimeBeforeTurnOff = 2,
                    ExpectedLoadingResourcesAfterTurnOff = 5
                }).SetName("ShouldReturnResources_Case1");

            // 2
            yield return new TestCaseData(new ConverterCreationData
                {
                    LoadingCapacity = 5,
                    UnloadingCapacity = 5,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 4,
                },
                new ConverterTurnedOffTestData
                {
                    DeltaTimeBeforeTurnOff = 3,
                    ExpectedLoadingResourcesAfterTurnOff = 4
                }).SetName("ShouldReturnResources_Case2");
        }

        public class ConverterTurnedOffTestData
        {
            public float DeltaTimeBeforeTurnOff { get; set; }
            public int ExpectedLoadingResourcesAfterTurnOff { get; set; }
        }
    }
}