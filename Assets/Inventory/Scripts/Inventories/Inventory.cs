using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;
        
        private readonly int width;
        private readonly int height;
        private readonly Item[,] grid;
        private readonly Dictionary<Item, Vector2Int> items;
        
        public int Width => width;
        public int Height => height;
        public int Count => items.Count;
        
        public Inventory(int width, int height)
        {
            ValidateDimensions(width, height);

            this.width = width;
            this.height = height;
            grid = new Item[width, height];
            items = new Dictionary<Item, Vector2Int>();
        }

        public Inventory(int width, int height, params KeyValuePair<Item, Vector2Int>[] items)
            : this(width, height) => AddItems(items);

        public Inventory(int width, int height, params Item[] items)
            : this(width, height) => AddItems(items);

        public Inventory(int width, int height, IEnumerable<KeyValuePair<Item, Vector2Int>> items)
            : this(width, height) => AddItems(items);

        public Inventory(int width, int height, IEnumerable<Item> items)
            : this(width, height) => AddItems(items);
    }
}
