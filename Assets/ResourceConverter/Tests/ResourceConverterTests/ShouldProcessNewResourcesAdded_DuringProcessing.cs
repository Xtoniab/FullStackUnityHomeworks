using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterNewResourcesDuringProcessingTestCases))]
        public void Converter_ShouldProcessNewResourcesAdded_DuringProcessing(
            ConverterCreationData converterData, ConverterNewResourcesDuringProcessingTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.DeltaTimes[0]);
            loadingArea.AddResources(data.AdditionalResources);
            converter.Update(data.DeltaTimes[1]);
            converter.Update(data.DeltaTimes[2]);
            converter.TurnOff();

            // Assert
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterNewResourcesDuringProcessingTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 3,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 3
                },
                new ConverterNewResourcesDuringProcessingTestData
                {
                    AdditionalResources = 6,
                    DeltaTimes = new[] { 2f, 3f, 5f },
                    ExpectedUnloadingResources = 4,
                    ExpectedLoadingResources = 3
                }).SetName("ShouldProcessNewResources_DuringProcessing_Case1");
        }

        public class ConverterNewResourcesDuringProcessingTestData
        {
            public int AdditionalResources { get; set; }
            public float[] DeltaTimes { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}