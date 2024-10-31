using System;
using UnityEngine;

namespace Inventories
{
    public sealed partial class Inventory
    {
        private void ValidateDimensions(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException("Inventory dimensions must be positive.");
        }

        private void ValidateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!ItemHasValidSize(item.Size))
                throw new ArgumentException("Item has invalid size.", nameof(item));
        }

        private void ValidatePosition(Vector2Int position)
        {
            if (!IsValidPosition(position))
                throw new IndexOutOfRangeException( "Position is out of range.");
        }

        private bool IsValidPosition(Vector2Int position) =>
            position.x >= 0 && position.x < width &&
            position.y >= 0 && position.y < height;

        private bool ItemHasValidSize(Vector2Int size) =>
            size.x > 0 && size.y > 0 && size.x <= width && size.y <= height;
    }
}