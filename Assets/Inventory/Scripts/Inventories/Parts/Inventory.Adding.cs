using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        
        private void AddItems(IEnumerable<KeyValuePair<Item, Vector2Int>> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var kvp in items)
            {
                AddItem(kvp.Key, kvp.Value);
            }
        }
        
        private void AddItems(IEnumerable<Item> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item);
            }
        }
        
        public bool CanAddItem(Item item, Vector2Int position)
        {
            if (item == null)
                return false;

            ValidateItem(item);

            if (!IsValidPosition(position) || items.ContainsKey(item))
                return false;

            return CanPlaceItem(item, position);
        }

        public bool CanAddItem(Item item, int posX, int posY) =>
            CanAddItem(item, new Vector2Int(posX, posY));

        public bool AddItem(Item item, Vector2Int position)
        {
            if (item == null)
                return false;

            ValidateItem(item);

            if (!IsValidPosition(position) || items.ContainsKey(item) || !CanPlaceItem(item, position))
                return false;

            PlaceItem(item, position);
            OnAdded?.Invoke(item, position);
            return true;
        }

        public bool AddItem(Item item, int posX, int posY) =>
            AddItem(item, new Vector2Int(posX, posY));

        public bool CanAddItem(Item item)
        {
            if (item == null || items.ContainsKey(item))
                return false;

            return FindFreePosition(item.Size, out _);
        }

        public bool AddItem(Item item)
        {
            if (item == null)
                return false;

            if (FindFreePosition(item.Size, out var freePosition))
                return AddItem(item, freePosition);

            return false;
        }

        private bool CanPlaceItem(Item item, Vector2Int position)
        {
            var itemSize = item.Size;

            if (position.x + itemSize.x > width || position.y + itemSize.y > height)
                return false;

            for (var x = position.x; x < position.x + itemSize.x; x++)
            {
                for (var y = position.y; y < position.y + itemSize.y; y++)
                {
                    if (grid[x, y] != null && grid[x, y] != item)
                        return false;
                }
            }

            return true;
        }

        private void PlaceItem(Item item, Vector2Int position)
        {
            var itemSize = item.Size;

            for (var x = position.x; x < position.x + itemSize.x; x++)
            {
                for (var y = position.y; y < position.y + itemSize.y; y++)
                {
                    grid[x, y] = item;
                }
            }

            items[item] = position;
        }
    }
}
