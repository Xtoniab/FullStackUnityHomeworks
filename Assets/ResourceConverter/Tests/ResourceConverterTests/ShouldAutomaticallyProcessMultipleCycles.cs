using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterMultipleCyclesTestCases))]
        public void Converter_ShouldAutomaticallyProcessMultipleCycles(
            ConverterCreationData converterData, ConverterMultipleCyclesTestData data)
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

        public static IEnumerable<TestCaseData> ConverterMultipleCyclesTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 6,
                },
                new ConverterMultipleCyclesTestData
                {
                    TotalDeltaTime = 15,
                    ExpectedUnloadingResources = 3,
                    ExpectedLoadingResources = 0
                }).SetName("ShouldProcessMultipleCycles_Case1");

            // 2
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 9,
                },
                new ConverterMultipleCyclesTestData
                {
                    TotalDeltaTime = 15,
                    ExpectedUnloadingResources = 6,
                    ExpectedLoadingResources = 0
                }).SetName("ShouldProcessMultipleCycles_Case2");

            // 3
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 1,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 5,
                },
                new ConverterMultipleCyclesTestData
                {
                    TotalDeltaTime = 10,
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 3
                }).SetName("ShouldProcessMultipleCycles_Case3");
        }

        public class ConverterMultipleCyclesTestData
        {
            public float TotalDeltaTime { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}