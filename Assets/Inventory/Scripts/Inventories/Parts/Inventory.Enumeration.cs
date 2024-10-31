using System.Collections;
using System.Collections.Generic;

namespace Inventories
{
    public sealed partial class Inventory : IEnumerable<Item>
    {
        public IEnumerator<Item> GetEnumerator() =>
            items.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}