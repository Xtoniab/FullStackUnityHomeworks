using System;

namespace ResourceConverters
{
    public class ResourceArea
    {
        public int Capacity { get; }
        public int ResourceCount { get; private set; }

        public ResourceArea(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity cannot be negative.", nameof(capacity));

            Capacity = capacity;
            ResourceCount = 0;
        }

        public ResourceArea(int capacity, int initialResources) : this(capacity)
        {
            if (initialResources < 0)
                throw new ArgumentException("Initial resources cannot be negative.", nameof(initialResources));

            // Написать тест на это
            if (initialResources > Capacity)
                throw new ArgumentException("Initial resources cannot exceed capacity.", nameof(initialResources));

            ResourceCount = Math.Min(initialResources, Capacity);
        }

        public int AddResources(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            var spaceAvailable = Capacity - ResourceCount;
            var resourcesAccepted = Math.Min(amount, spaceAvailable);
            var excessResources = amount - resourcesAccepted;

            ResourceCount += resourcesAccepted;

            return excessResources;
        }

        public int RemoveResources(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            var resourcesRemoved = Math.Min(amount, ResourceCount);
            ResourceCount -= resourcesRemoved;
            return resourcesRemoved;
        }
    }
}