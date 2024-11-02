using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterUnloadingAreaFullTestCases))]
        public void Converter_ShouldNotStartNewCycle_WhenUnloadingAreaHasNoEnoughSpace(
            ConverterCreationData converterData, ConverterUnloadingAreaFullTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.TotalDeltaTime);
            converter.TurnOff();

            // Assert
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterUnloadingAreaFullTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 2,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 6
                },
                new ConverterUnloadingAreaFullTestData
                {
                    TotalDeltaTime = 15,
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 2
                }).SetName("ShouldNotStartNewCycle_UnloadingAreaFull_Case1");

            // 2
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 1,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 9
                },
                new ConverterUnloadingAreaFullTestData
                {
                    TotalDeltaTime = 15,
                    ExpectedUnloadingResources = 0,
                    ExpectedLoadingResources = 9
                }).SetName("ShouldNotStartNewCycle_UnloadingAreaFull_Case2");

            // 3
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 3,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 9
                },
                new ConverterUnloadingAreaFullTestData
                {
                    TotalDeltaTime = 15,
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 6
                }).SetName("ShouldNotStartNewCycle_UnloadingAreaFull_Case3");
        }

        public class ConverterUnloadingAreaFullTestData
        {
            public float TotalDeltaTime { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}