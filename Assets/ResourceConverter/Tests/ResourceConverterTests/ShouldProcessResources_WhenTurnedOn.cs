using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterProcessResourcesTestCases))]
        public void Converter_ShouldProcessResources_WhenTurnedOn(
            ConverterCreationData converterData, ConverterProcessResourcesTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.DeltaTime);
            converter.TurnOff();

            // Assert
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterProcessResourcesTestCases()
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
                    InitialLoadingResources = 5,
                },
                new ConverterProcessResourcesTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 1,
                    ExpectedLoadingResources = 3
                }).SetName("ShouldProcessResources_Case1");

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
                }, new ConverterProcessResourcesTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 6
                }).SetName("ShouldProcessResources_Case2");

            // 3
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 4,
                    ResourcesDeliveredPerCycle = 3,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 8,
                }, new ConverterProcessResourcesTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 3,
                    ExpectedLoadingResources = 4
                }).SetName("ShouldProcessResources_Case3");
        }

        public class ConverterProcessResourcesTestData
        {
            public float DeltaTime { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}