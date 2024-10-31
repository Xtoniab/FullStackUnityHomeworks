using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        private readonly int width;
        private readonly int height;
        
        // Здесь мы немного жертвуем памятью, но за счёт двух коллекций выигрываем в рантайме в скорости поиска, проверок на наличие, итп
        private readonly Item[,] grid;
        private readonly Dictionary<Item, Vector2Int> items;

        public int Width => width;
        public int Height => height;
        public int Count => items.Count;

        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Inventory dimensions must be positive.");

            this.width = width;
            this.height = height;
            grid = new Item[width, height];
            items = new Dictionary<Item, Vector2Int>();
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var kvp in items)
            {
                AddItem(kvp.Key, kvp.Value);
            }
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var kvp in items)
            {
                AddItem(kvp.Key, kvp.Value);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (item == null)
                return false;

            if (!ItemHasValidSize(item.Size))
                throw new ArgumentException("Item has invalid size.");

            if (!IsValidPosition(position))
                return false;

            if (items.ContainsKey(item))
                return false;

            return CanPlaceItemAtPosition(item, position);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY) =>
            CanAddItem(item, new Vector2Int(posX, posY));

        private bool CanPlaceItemAtPosition(Item item, Vector2Int position)
        {
            var itemWidth = item.Size.x;
            var itemHeight = item.Size.y;

            if (position.x + itemWidth > width || position.y + itemHeight > height)
                return false;

            for (var x = position.x; x < position.x + itemWidth; x++)
            {
                for (var y = position.y; y < position.y + itemHeight; y++)
                {
                    if (grid[x, y] != null && !grid[x, y].Equals(item))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if (item == null)
                return false;

            if (!ItemHasValidSize(item.Size))
                throw new ArgumentException("Item has invalid size.");

            if (items.ContainsKey(item))
                return false;

            if (!IsValidPosition(position))
                return false;

            if (!CanPlaceItemAtPosition(item, position))
                return false;

            PlaceItemAtPosition(item, position);
            OnAdded?.Invoke(item, position);
            return true;
        }

        private bool ItemHasValidSize(Vector2Int size)
        {
            if (size.x <= 0 || size.y <= 0)
                return false;

            return size.x <= width && size.y <= height;
        }

        public bool AddItem(in Item item, in int posX, in int posY) =>
            AddItem(item, new Vector2Int(posX, posY));

        private void PlaceItemAtPosition(Item item, Vector2Int position)
        {
            var itemWidth = item.Size.x;
            var itemHeight = item.Size.y;

            for (var x = position.x; x < position.x + itemWidth; x++)
            {
                for (var y = position.y; y < position.y + itemHeight; y++)
                {
                    grid[x, y] = item;
                }
            }

            items[item] = position;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item == null)
                return false;

            if (items.ContainsKey(item))
                return false;

            return FindFreePosition(item, out _);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null)
                return false;

            if (FindFreePosition(item, out var freePosition))
            {
                return AddItem(item, freePosition);
            }

            return false;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Item item, out Vector2Int freePosition)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return FindFreePosition(item.Size, out freePosition);
        }

        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (size.x <= 0 || size.y <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Size dimensions must be positive.");

            var itemWidth = size.x;
            var itemHeight = size.y;

            for (var y = 0; y <= height - itemHeight; y++)
            {
                for (var x = 0; x <= width - itemWidth; x++)
                {
                    if (IsAreaFree(new Vector2Int(x, y), size))
                    {
                        freePosition = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            freePosition = new Vector2Int(0, 0);
            return false;
        }

        public bool FindFreePosition(in int sizeX, int sizeY, out Vector2Int freePosition) =>
            FindFreePosition(new Vector2Int(sizeX, sizeY), out freePosition);

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

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            if (item == null)
                return false;

            return items.ContainsKey(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            if (!IsValidPosition(position))
                return false;

            return grid[position.x, position.y] != null;
        }

        public bool IsOccupied(in int x, in int y) =>
            IsOccupied(new Vector2Int(x, y));

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            if (!IsValidPosition(position))
                return false;

            return grid[position.x, position.y] == null;
        }

        public bool IsFree(in int x, in int y) =>
            IsFree(new Vector2Int(x, y));

        private bool IsValidPosition(Vector2Int position) =>
            position.x >= 0 && position.x < width &&
            position.y >= 0 && position.y < height;

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return RemoveItem(item, out _);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = new Vector2Int(0, 0);

            if (item == null)
                return false;

            if (!items.TryGetValue(item, out position))
                return false;

            RemoveItemFromGrid(item, position);
            OnRemoved?.Invoke(item, position);
            return true;
        }

        private void RemoveItemFromGrid(Item item, Vector2Int position)
        {
            var itemWidth = item.Size.x;
            var itemHeight = item.Size.y;

            for (var x = position.x; x < position.x + itemWidth; x++)
            {
                for (var y = position.y; y < position.y + itemHeight; y++)
                {
                    grid[x, y] = null;
                }
            }

            items.Remove(item);
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (!IsValidPosition(position))
                throw new IndexOutOfRangeException("Position is out of range.");

            var item = grid[position.x, position.y];

            if (item == null)
                throw new NullReferenceException("No item at specified position.");

            return item;
        }

        public Item GetItem(in int x, in int y) =>
            GetItem(new Vector2Int(x, y));

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            item = null;

            if (!IsValidPosition(position))
                return false;

            item = grid[position.x, position.y];
            return item != null;
        }

        public bool TryGetItem(in int x, in int y, out Item item) =>
            TryGetItem(new Vector2Int(x, y), out item);

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
                throw new NullReferenceException(nameof(item));

            if (!items.TryGetValue(item, out Vector2Int origin))
                throw new KeyNotFoundException("Item not found in inventory.");

            var itemWidth = item.Size.x;
            var itemHeight = item.Size.y;
            var positions = new Vector2Int[itemWidth * itemHeight];
            var index = 0;

            for (var x = origin.x; x < origin.x + itemWidth; x++)
            {
                for (var y = origin.y; y < origin.y + itemHeight; y++)
                {
                    positions[index++] = new Vector2Int(x, y);
                }
            }

            return positions;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (item == null)
                return false;

            if (!items.ContainsKey(item))
                return false;

            positions = GetPositions(item);
            return positions?.Length > 0;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (items.Count == 0)
                return;

            Array.Clear(grid, 0, grid.Length);
            items.Clear();
            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
            => items.Keys.Count(item => item.Name == name);

        /// <summary>
        /// Moves a specified item to a target position if it exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int newPosition)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!IsValidPosition(newPosition))
                return false;

            if (!items.TryGetValue(item, out Vector2Int oldPosition))
                return false;

            if (!CanPlaceItemAtPosition(item, newPosition))
                return false;

            RemoveItemFromGrid(item, oldPosition);
            PlaceItemAtPosition(item, newPosition);
            OnMoved?.Invoke(item, newPosition);
            return true;
        }

        /// <summary>
        /// Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            // Не знаю насколько оптималке алгоритм, но тесты проходит👍
            var itemsList = items.Keys.ToList();

            foreach (var item in itemsList)
            {
                RemoveItem(item);
            }

            itemsList.Sort((itemA, itemB) =>
            {
                var areaA = itemA.Size.x * itemA.Size.y;
                var areaB = itemB.Size.x * itemB.Size.y;
                return areaB.CompareTo(areaA);
            });

            foreach (var item in itemsList)
            {
                var itemPlaced = false;
                for (var y = 0; y < height && !itemPlaced; y++)
                {
                    for (var x = 0; x < width && !itemPlaced; x++)
                    {
                        var position = new Vector2Int(x, y);
                        if (CanAddItem(item, position))
                        {
                            AddItem(item, position);
                            itemPlaced = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            if (matrix.GetLength(0) != width || matrix.GetLength(1) != height)
                throw new ArgumentException("Matrix size does not match inventory size.");

            Array.Copy(grid, matrix, grid.Length);
        }

        public IEnumerator<Item> GetEnumerator() =>
            items.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}