namespace ResourceConverters
{
    public partial class ResourceConverterTests
    {
        public class ConverterCreationData
        {
            public int LoadingCapacity { get; set; }
            public int UnloadingCapacity { get; set; }
            public int ResourcesTakenPerCycle { get; set; }
            public int ResourcesDeliveredPerCycle { get; set; }
            public float ConversionTimeSeconds { get; set; }
            public int InitialLoadingResources { get; set; }
            public int InitialUnloadingResources { get; set; }
        }

        private static (ResourceConverter, ResourceArea, ResourceArea) CreateConverter(ConverterCreationData data)
        {
            var loadingArea = new ResourceArea(data.LoadingCapacity, data.InitialLoadingResources);
            var unloadingArea = new ResourceArea(data.UnloadingCapacity, data.InitialUnloadingResources);

            var converter = new ResourceConverter(
                loadingArea,
                unloadingArea,
                data.ResourcesTakenPerCycle,
                data.ResourcesDeliveredPerCycle,
                data.ConversionTimeSeconds);

            return (converter, loadingArea, unloadingArea);
        }
    }
}