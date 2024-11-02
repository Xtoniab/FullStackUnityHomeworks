using System;

namespace ResourceConverters
{
    public class ResourceConverter
    {
        private readonly ResourceArea loadingArea;
        private readonly ResourceArea unloadingArea;
        private readonly int resourcesTakenPerCycle;
        private readonly int resourcesDeliveredPerCycle;
        private readonly float conversionTimeSeconds;

        public bool IsOn { get; private set; }
        public bool Processing => ResourcesInProcess > 0;
        public int ResourcesInProcess { get; private set; }

        private float currentTime;

        public ResourceConverter(
            ResourceArea loadingArea,
            ResourceArea unloadingArea,
            int resourcesTakenPerCycle,
            int resourcesDeliveredPerCycle,
            float conversionTimeSeconds)
        {
            this.loadingArea = loadingArea ?? throw new ArgumentNullException(nameof(loadingArea), "Loading area cannot be null.");
            this.unloadingArea = unloadingArea ?? throw new ArgumentNullException(nameof(unloadingArea), "Unloading area cannot be null.");
            
            if (resourcesTakenPerCycle <= 0)
                throw new ArgumentException("Resources taken per cycle must be positive.", nameof(resourcesTakenPerCycle));
            
            if (resourcesDeliveredPerCycle <= 0)
                throw new ArgumentException("Resources delivered per cycle must be positive.", nameof(resourcesDeliveredPerCycle));
            
            if (conversionTimeSeconds <= 0)
                throw new ArgumentException("Conversion time must be positive.", nameof(conversionTimeSeconds));

            this.resourcesTakenPerCycle = resourcesTakenPerCycle;
            this.resourcesDeliveredPerCycle = resourcesDeliveredPerCycle;
            this.conversionTimeSeconds = conversionTimeSeconds;
        }

        public void TurnOn()
        {
            if (IsOn)
                return;

            IsOn = true;
            currentTime = 0;
            TryStartNewProcess();
        }

        public void TurnOff()
        {
            if (!IsOn)
                return;

            IsOn = false;
            if (Processing)
            {
                loadingArea.AddResources(ResourcesInProcess);
                ResourcesInProcess = 0;
                currentTime = 0;
            }
        }

        public void Update(float deltaTime)
        {
            if (!IsOn)
                return;
            
            if (Processing || TryStartNewProcess())
            {
                currentTime += deltaTime;

                while (currentTime >= conversionTimeSeconds)
                {
                    currentTime -= conversionTimeSeconds;
                    
                    DeliverResources();
                    
                    if (!TryStartNewProcess())
                        break;
                }
            }
        }

        private bool TryStartNewProcess()
        {
            var hasLoadingResources = loadingArea.ResourceCount >= resourcesTakenPerCycle;
            var hasSpaceInUnloadingArea = unloadingArea.Capacity - unloadingArea.ResourceCount >= resourcesDeliveredPerCycle;
            
            if (hasLoadingResources && hasSpaceInUnloadingArea)
            {
                ResourcesInProcess = loadingArea.RemoveResources(resourcesTakenPerCycle);
            }
            
            return ResourcesInProcess > 0;
        }

        private void DeliverResources()
        {
            unloadingArea.AddResources(resourcesDeliveredPerCycle);
            ResourcesInProcess = 0;
        }
    }
}
