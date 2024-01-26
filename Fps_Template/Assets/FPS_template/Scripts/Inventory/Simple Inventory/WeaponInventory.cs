using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : InventoryBase
{
    public override void AddItem(InventoryItemBase item)
    {
        // check if we already have that weapon
        if (_items.Contains(item)) return;

        base.AddItem(item);
    }
}
