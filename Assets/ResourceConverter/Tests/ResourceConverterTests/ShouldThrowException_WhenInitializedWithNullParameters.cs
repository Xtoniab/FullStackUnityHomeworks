using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        [Test, TestCaseSource(nameof(InitializedWithNullTestCases))]
        public void Converter_ShouldThrowException_WhenInitializedWithNullParameters(InitializedWithNullTestData data)
        {
            // Arrange
            Action action = () => new ResourceConverter(
                data.LoadingArea,
                data.UnloadingArea,
                1,
                1,
                1);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*cannot be null*");
        }

        private static IEnumerable<TestCaseData> InitializedWithNullTestCases()
        {
            yield return new TestCaseData(new InitializedWithNullTestData
            {
                LoadingArea = null,
                UnloadingArea = new ResourceArea(1)
            }).SetName("ShouldThrowException_WhenLoadingAreaIsNull_LoadingAreaNull");

            yield return new TestCaseData(new InitializedWithNullTestData
            {
                LoadingArea = new ResourceArea(1),
                UnloadingArea = null
            }).SetName("ShouldThrowException_WhenUnloadingAreaIsNull_UnloadingAreaNull");

            yield return new TestCaseData(new InitializedWithNullTestData
            {
                LoadingArea = null,
                UnloadingArea = null
            }).SetName("ShouldThrowException_WhenBothAreasAreNull_BothAreasNull");
        }

        public class InitializedWithNullTestData
        {
            public ResourceArea LoadingArea { get; set; }
            public ResourceArea UnloadingArea { get; set; }
        }
    }
}