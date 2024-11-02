using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterInsufficientResourcesTestCases))]
        public void Converter_ShouldNotProcess_WhenLoadingAreaHasInsufficientResources(
            ConverterCreationData converterData, ConverterInsufficientResourcesTestData data)
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

        public static IEnumerable<TestCaseData> ConverterInsufficientResourcesTestCases()
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
                    InitialLoadingResources = 2,
                },
                new ConverterInsufficientResourcesTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 0,
                    ExpectedLoadingResources = 2
                }).SetName("ShouldNotProcess_InsufficientResources_Case1");

            // 2
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 5,
                    UnloadingCapacity = 5,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 1,
                },
                new ConverterInsufficientResourcesTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 0,
                    ExpectedLoadingResources = 1
                }).SetName("ShouldNotProcess_InsufficientResources_Case2");
        }

        public class ConverterInsufficientResourcesTestData
        {
            public float DeltaTime { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}