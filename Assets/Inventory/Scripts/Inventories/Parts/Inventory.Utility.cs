using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        public void Clear()
        {
            if (items.Count == 0)
                return;

            Array.Clear(grid, 0, grid.Length);
            items.Clear();
            OnCleared?.Invoke();
        }

        public void ReorganizeSpace()
        {
            var itemsList = new List<Item>(items.Keys);

            foreach (var item in itemsList)
                RemoveItem(item);

            itemsList.Sort((a, b) => (b.Size.x * b.Size.y).CompareTo(a.Size.x * a.Size.y));

            foreach (var item in itemsList)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (AddItem(item, new Vector2Int(x, y)))
                            goto NextItem;
                    }
                }
                NextItem:;
            }
        }

        public void CopyTo(Item[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            if (matrix.GetLength(0) != width || matrix.GetLength(1) != height)
                throw new ArgumentException("Matrix size does not match inventory size.");

            Array.Copy(grid, matrix, grid.Length);
        }
    }
}