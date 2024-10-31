using System;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        public bool MoveItem(Item item, Vector2Int newPosition)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if(!IsValidPosition(newPosition))
                return false;

            if (!items.TryGetValue(item, out var oldPosition))
                return false;

            if (!CanPlaceItem(item, newPosition))
                return false; 

            RemoveItemFromGrid(item, oldPosition);
            PlaceItem(item, newPosition);
            OnMoved?.Invoke(item, newPosition);
            return true;
        }
    }
}