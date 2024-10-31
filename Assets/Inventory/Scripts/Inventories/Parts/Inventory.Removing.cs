using System;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        public bool RemoveItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return RemoveItem(item, out _);
        }

        public bool RemoveItem(Item item, out Vector2Int position)
        {
            position = default;

            if (item == null || !items.TryGetValue(item, out position))
                return false;

            RemoveItemFromGrid(item, position);
            OnRemoved?.Invoke(item, position);
            return true;
        }

        private void RemoveItemFromGrid(Item item, Vector2Int position)
        {
            var itemSize = item.Size;

            for (var x = position.x; x < position.x + itemSize.x; x++)
            {
                for (var y = position.y; y < position.y + itemSize.y; y++)
                {
                    grid[x, y] = null;
                }
            }

            items.Remove(item);
        }
    }
}