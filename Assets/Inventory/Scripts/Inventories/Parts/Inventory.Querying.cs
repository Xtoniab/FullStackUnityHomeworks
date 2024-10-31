using System;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    // Тут запросы к инвентарю лежат, кроме получения элементов
    public sealed partial class Inventory
    {
        public bool IsOccupied(Vector2Int position)
        {
            if (!IsValidPosition(position))
                return false;

            return grid[position.x, position.y] != null;
        }

        public bool IsOccupied(int x, int y) =>
            IsOccupied(new Vector2Int(x, y));

        public bool IsFree(Vector2Int position)
        {
            if (!IsValidPosition(position))
                return false;

            return grid[position.x, position.y] == null;
        }

        public bool IsFree(int x, int y) =>
            IsFree(new Vector2Int(x, y));

        private bool IsAreaFree(Vector2Int position, Vector2Int size)
        {
            if (position.x + size.x > width || position.y + size.y > height)
                return false;

            for (var x = position.x; x < position.x + size.x; x++)
            {
                for (var y = position.y; y < position.y + size.y; y++)
                {
                    if (grid[x, y] != null)
                        return false;
                }
            }

            return true;
        }
        
        public bool FindFreePosition(Item item, out Vector2Int freePosition)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return FindFreePosition(item.Size, out freePosition);
        }

        public bool FindFreePosition(Vector2Int size, out Vector2Int freePosition)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size dimensions must be positive.");

            for (var y = 0; y <= height - size.y; y++)
            {
                for (var x = 0; x <= width - size.x; x++)
                {
                    if (IsAreaFree(new Vector2Int(x, y), size))
                    {
                        freePosition = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            freePosition = default;
            return false;
        }

        public bool FindFreePosition(int sizeX, int sizeY, out Vector2Int freePosition) =>
            FindFreePosition(new Vector2Int(sizeX, sizeY), out freePosition);
        
        
        public bool Contains(Item item)
        {
            if (item == null)
                return false;

            return items.ContainsKey(item);
        }

        public int GetItemCount(string name) =>
            items.Count(kvp => kvp.Key.Name == name);
    }
}
