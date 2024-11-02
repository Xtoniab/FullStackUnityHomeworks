using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterTimeAccumulatesTestCases))]
        public void Converter_ShouldProcessMultipleCycles_WhenTimeAccumulates(
            ConverterCreationData converterData, ConverterTimeAccumulatesTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();

            foreach (var deltaTime in data.DeltaTimes)
            {
                converter.Update(deltaTime);
            }

            converter.TurnOff();

            // Assert
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterTimeAccumulatesTestCases()
        {
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 8
                },
                new ConverterTimeAccumulatesTestData
                {
                    DeltaTimes = new[] { 3f, 4f, 8f },
                    ExpectedUnloadingResources = 6,
                    ExpectedLoadingResources = 2
                }).SetName("ShouldProcessMultipleCycles_TimeAccumulates_Case1");
        }

        public class ConverterTimeAccumulatesTestData
        {
            public float[] DeltaTimes { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}