using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Random = System.Random;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterInitializationTestCases))]
        public void Converter_ShouldInitializeCorrectly(ConverterCreationData converterData)
        {
            // Arrange
            var (converter, loadingArea, unloadingArea) = CreateConverter(converterData);

            // Assert
            unloadingArea.Capacity.Should().Be(converterData.UnloadingCapacity);
            unloadingArea.ResourceCount.Should().Be(converterData.InitialUnloadingResources);

            loadingArea.Capacity.Should().Be(converterData.LoadingCapacity);
            loadingArea.ResourceCount.Should().Be(converterData.InitialLoadingResources);

            converter.IsOn.Should().BeFalse();
            converter.Processing.Should().BeFalse();
        }

        public static IEnumerable<TestCaseData> ConverterInitializationTestCases()
        {
            // :D
            var rand = new Random(333);
            for (var i = 0; i < 20; i++)
            {
                var loadingCapacity = rand.Next(0, 100);
                var unloadingCapacity = rand.Next(0, 100);
                var resourcesTakenPerCycle = rand.Next(1, 100);
                var resourcesDeliveredPerCycle = rand.Next(1, 100);
                var conversionTimeSeconds = rand.Next(1, 100);
                var initialLoadingResources = rand.Next(0, loadingCapacity + 1);
                var initialUnloadingResources = rand.Next(0, unloadingCapacity + 1);

                yield return new TestCaseData(
                    new ConverterCreationData
                    {
                        LoadingCapacity = loadingCapacity,
                        UnloadingCapacity = unloadingCapacity,
                        ResourcesTakenPerCycle = resourcesTakenPerCycle,
                        ResourcesDeliveredPerCycle = resourcesDeliveredPerCycle,
                        ConversionTimeSeconds = conversionTimeSeconds,
                        InitialLoadingResources = initialLoadingResources,
                        InitialUnloadingResources = initialUnloadingResources
                    }).SetName(
                    $"ShouldInitializeCorrectly(" +
                    $"{loadingCapacity}," +
                    $" {unloadingCapacity}," +
                    $" {resourcesTakenPerCycle}," +
                    $" {resourcesDeliveredPerCycle}," +
                    $" {conversionTimeSeconds}," +
                    $" {initialLoadingResources}," +
                    $" {initialUnloadingResources})");
            }
        }
    }
}