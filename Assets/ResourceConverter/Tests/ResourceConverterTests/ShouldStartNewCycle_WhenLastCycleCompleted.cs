using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterStartNextCycleTestCases))]
        public void Converter_ShouldStartNewCycle_WhenLastCycleCompleted(
            ConverterCreationData converterData, ConverterStartNextCycleTestData data)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Act
            converter.TurnOn();
            converter.Update(data.DeltaTime);
            // Не выключаем конвертер
            
            // Assert
            loadingArea.ResourceCount.Should().Be(data.ExpectedLoadingResources);
            unloadingArea.ResourceCount.Should().Be(data.ExpectedUnloadingResources);
        }

        public static IEnumerable<TestCaseData> ConverterStartNextCycleTestCases()
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
                new ConverterStartNextCycleTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 1,
                    ExpectedLoadingResources = 1
                }).SetName("ShouldStartNewCycle_Case1");

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
                }, new ConverterStartNextCycleTestData
                {
                    DeltaTime = 6,
                    ExpectedUnloadingResources = 2,
                    ExpectedLoadingResources = 3
                }).SetName("ShouldStartNewCycle_Case2");

            // 3
            yield return new TestCaseData(
                new ConverterCreationData
                {
                    LoadingCapacity = 10,
                    UnloadingCapacity = 10,
                    ResourcesTakenPerCycle = 4,
                    ResourcesDeliveredPerCycle = 3,
                    ConversionTimeSeconds = 4,
                    InitialLoadingResources = 8,
                }, new ConverterStartNextCycleTestData
                {
                    DeltaTime = 5,
                    ExpectedUnloadingResources = 3,
                    ExpectedLoadingResources = 0
                }).SetName("ShouldStartNewCycle_Case3");
        }

        public class ConverterStartNextCycleTestData
        {
            public float DeltaTime { get; set; }
            public int ExpectedUnloadingResources { get; set; }
            public int ExpectedLoadingResources { get; set; }
        }
    }
}