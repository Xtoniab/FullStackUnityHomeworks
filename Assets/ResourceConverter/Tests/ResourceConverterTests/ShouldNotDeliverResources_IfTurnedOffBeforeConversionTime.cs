using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterTurnedOffBeforeConversionTestCases))]
        public void Converter_ShouldNotDeliverResources_IfTurnedOffBeforeConversionTime(
            ConverterCreationData converterData, ConverterTurnedOffBeforeConversionTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.DeltaTimeBeforeTurnOff);
            converter.TurnOff();
            converter.Update(converterData.ConversionTimeSeconds - data.DeltaTimeBeforeTurnOff);

            // Assert
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterTurnedOffBeforeConversionTestCases()
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
                    InitialLoadingResources = 4,
                },
                new ConverterTurnedOffBeforeConversionTestData
                {
                    DeltaTimeBeforeTurnOff = 3,
                    ExpectedUnloadingResources = 0,
                    ExpectedLoadingResources = 4
                }).SetName("ShouldNotDeliver_IfTurnedOffBeforeConversion_Case1");

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
                new ConverterTurnedOffBeforeConversionTestData
                {
                    DeltaTimeBeforeTurnOff = 2,
                    ExpectedUnloadingResources = 0,
                    ExpectedLoadingResources = 3
                }).SetName("ShouldNotDeliver_IfTurnedOffBeforeConversion_Case2");
        }

        public class ConverterTurnedOffBeforeConversionTestData
        {
            public float DeltaTimeBeforeTurnOff { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}