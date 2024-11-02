using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterAccumulateTimeTestCases))]
        public void Converter_ShouldAccumulateTime_WhenDeltaTimeLessThanConversionTime(
            ConverterCreationData converterData, ConverterAccumulateTimeTestData data)
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

        public static IEnumerable<TestCaseData> ConverterAccumulateTimeTestCases()
        {
            // 1
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 5,
                    UnloadingCapacity = 5,
                    ResourcesTakenPerCycle = 2,
                    ResourcesDeliveredPerCycle = 2,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 4
                },
                new ConverterAccumulateTimeTestData
                {
                    DeltaTimes = new[] { 2f, 2f, 1f },
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 2
                }).SetName("ShouldAccumulateTime_DeltaTimeLessThanConversion_Case1");

            // 2
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 5,
                    UnloadingCapacity = 5,
                    ResourcesTakenPerCycle = 1,
                    ResourcesDeliveredPerCycle = 1,
                    ConversionTimeSeconds = 5,
                    InitialLoadingResources = 3,
                },
                new ConverterAccumulateTimeTestData
                {
                    DeltaTimes = new[] { 1f, 2f, 2f },
                    ExpectedUnloadingResources = 1,
                    ExpectedLoadingResources = 2
                }).SetName("ShouldAccumulateTime_DeltaTimeLessThanConversion_Case2");
        }

        public class ConverterAccumulateTimeTestData
        {
            public float[] DeltaTimes { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}