using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryBase : MonoBehaviour
{
    [SerializeField] protected InventoryIdentifiers _inventoryIdentifier;
    [SerializeField] protected List<InventoryItemBase> _items;

    public virtual void AddItem(InventoryItemBase item)
    {
        _items.Add(item);
    }

    public virtual void RemoveItem(InventoryItemBase item)
    {
        _items.Remove(item);
    }

    public virtual InventoryIdentifiers GetInventoryIdentifier() => _inventoryIdentifier;

    public virtual InventoryItemBase GetItem(int itemIndex) => _items[itemIndex];

    public virtual bool IsInventoryEmpty()
    {
        if (_items == null || _items.Count == 0)
        {
            Debug.Log("Inventory is empty");
            return true;
        }

        Debug.Log("Inventory is not empty");
        return false;
    }

    public virtual int GetItemsCount() => _items.Count;
}
