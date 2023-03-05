using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryBase : MonoBehaviour
{
    [SerializeField] protected string _inventoryIdentifier;
    [SerializeField] protected List<InventoryItemBase> _items;

    public virtual void AddItem(InventoryItemBase item)
    {
        _items.Add(item);
    }

    public virtual void RemoveItem(InventoryItemBase item)
    {
        _items.Remove(item);
    }

    public virtual string GetInventoryIdentifier() => _inventoryIdentifier;
}
