using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Collider))]
public class PickUpInteractable : Interactable
{
    [SerializeField] private InventoryItemBase _item;
    [SerializeField] private float _disableDelay;

    protected override void Interact(Interactable interactable)
    {
        //Debug.Log("PickpuInteractable - Interact - STARTED");
        if (interactable != this)
            return;
        
        AddItemToInventory();
        //base.Interact(interactable); // TODO think if this is needed


        //Debug.Log("PickpuInteractable - Interact - END");
        //Destroy(this.gameObject, 0.2f);
        Disable();
        StartCoroutine(DisableCompletelyDelay());
    }

    private void AddItemToInventory()
    {
        //get inventory and add the item
        var inventory = InventoriesManager.Instance.GetInventory(_item.inventoryIdentifier);
        
        
        // if this item is the first weapon the player takes, auto-equip it            
        if(inventory.GetInventoryIdentifier() == InventoryIdentifiers.WEAPONS_INVENTORY && inventory.IsInventoryEmpty())
        {
            inventory.AddItem(_item);

            Debug.Log("Auto equipping");
            EventAggregator.GetEvent<EquipWeaponEvent>().Publish(0); // we re passing 0 because it's the first weapon

            return;
        }

        inventory.AddItem(_item);
    }

    private void Disable()
    {
        Debug.Log("Disable" + DateTime.Now);
        //disable or destroy or pool
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;        
    }

    private IEnumerator DisableCompletelyDelay()
    {
        yield return new WaitForSeconds(_disableDelay);

        Debug.Log("DisableCompletelyDelay" + DateTime.Now);
        this.gameObject.SetActive(false);
    }
}
