using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(ConverterInvalidParametersTestCases))]
        public void Converter_ShouldThrowException_WhenInitializedWithInvalidParameters(ConverterInvalidParametersTestData data)
        {
            // Arrange
            var loadingArea = new ResourceArea(data.LoadingCapacity);
            var unloadingArea = new ResourceArea(data.UnloadingCapacity);

            // Act
            Action action = () => new ResourceConverter(
                loadingArea,
                unloadingArea,
                data.ResourcesTakenPerCycle,
                data.ResourcesDeliveredPerCycle,
                data.ConversionTimeSeconds);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*positive*");
        }

        public static IEnumerable<TestCaseData> ConverterInvalidParametersTestCases()
        {
            yield return new TestCaseData(new ConverterInvalidParametersTestData
            {
                LoadingCapacity = 5,
                UnloadingCapacity = 5,
                ResourcesTakenPerCycle = -2,
                ResourcesDeliveredPerCycle = 1,
                ConversionTimeSeconds = 5
            }).SetName("ShouldThrowException_InvalidResourcesTakenPerCycle");

            yield return new TestCaseData(new ConverterInvalidParametersTestData
            {
                LoadingCapacity = 5,
                UnloadingCapacity = 5,
                ResourcesTakenPerCycle = 2,
                ResourcesDeliveredPerCycle = -1,
                ConversionTimeSeconds = 5
            }).SetName("ShouldThrowException_InvalidResourcesDeliveredPerCycle");

            yield return new TestCaseData(new ConverterInvalidParametersTestData
            {
                LoadingCapacity = 5,
                UnloadingCapacity = 5,
                ResourcesTakenPerCycle = 2,
                ResourcesDeliveredPerCycle = 1,
                ConversionTimeSeconds = -5
            }).SetName("ShouldThrowException_InvalidConversionTimeSeconds");
        }

        public class ConverterInvalidParametersTestData
        {
            public int LoadingCapacity { get; set; }
            public int UnloadingCapacity { get; set; }
            public int ResourcesTakenPerCycle { get; set; }
            public int ResourcesDeliveredPerCycle { get; set; }
            public float ConversionTimeSeconds { get; set; }
        }
    }
}