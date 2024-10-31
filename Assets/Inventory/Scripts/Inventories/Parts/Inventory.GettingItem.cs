using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        public Item GetItem(int x, int y) =>
            GetItem(new Vector2Int(x, y));
        
        public Item GetItem(Vector2Int position)
        {
            ValidatePosition(position);

            var item = grid[position.x, position.y];
            if (item == null)
                throw new NullReferenceException("No item at specified position.");

            return item;
        }

        public bool TryGetItem(Vector2Int position, out Item item)
        {
            item = null;

            if (!IsValidPosition(position))
                return false;

            item = grid[position.x, position.y];
            return item != null;
        }

        public bool TryGetItem(int x, int y, out Item item) =>
            TryGetItem(new Vector2Int(x, y), out item);

        public Vector2Int[] GetPositions(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!items.TryGetValue(item, out var origin))
                throw new KeyNotFoundException("Item not found in inventory.");

            var positions = new List<Vector2Int>();
            var itemSize = item.Size;

            for (var x = origin.x; x < origin.x + itemSize.x; x++)
            {
                for (var y = origin.y; y < origin.y + itemSize.y; y++)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }

            return positions.ToArray();
        }

        public bool TryGetPositions(Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (item == null || !items.ContainsKey(item))
                return false;

            positions = GetPositions(item);
            return positions.Length > 0;
        }
    }
}