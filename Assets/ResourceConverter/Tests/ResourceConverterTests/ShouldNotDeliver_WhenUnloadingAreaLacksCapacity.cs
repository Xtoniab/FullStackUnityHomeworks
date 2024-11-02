using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterUnloadingAreaLacksCapacityTestCases))]
        public void Converter_ShouldNotDeliver_WhenUnloadingAreaLacksCapacity(
            ConverterCreationData converterData, ConverterUnloadingAreaLacksCapacityTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(converterData.ConversionTimeSeconds);
            converter.TurnOff();

            // Assert
            unloadingArea.ResourceCount.Should().Be(unloadingArea.Capacity);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterUnloadingAreaLacksCapacityTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 1,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 4,
                    InitialUnloadingResources = 1,
                },
                new ConverterUnloadingAreaLacksCapacityTestData
                {
                    ExpectedLoadingResources = 4
                }).SetName("ShouldNotDeliver_UnloadingAreaFull_Case1");

            // 2
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 0,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 6,
                    InitialUnloadingResources = 0,
                },
                new ConverterUnloadingAreaLacksCapacityTestData
                {
                    ExpectedLoadingResources = 6
                }).SetName("ShouldNotDeliver_UnloadingAreaFull_Case2");
        }

        public class ConverterUnloadingAreaLacksCapacityTestData
        {
            public int ExpectedLoadingResources { get; set; }
        }
    }
}