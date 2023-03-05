using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        base.Interact(interactable);


        //Debug.Log("PickpuInteractable - Interact - END");
        Disable();
        StartCoroutine(DisableCompletelyDelay());
    }

    private void AddItemToInventory()
    {
        //get inventory and add the item
        var inventory = InventoriesManager.Instance.GetInventory(_item.inventoryIdentifier);
        inventory.AddItem(_item);
    }

    private void Disable()
    {
        //disable or destroy or pool
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;        
    }

    private IEnumerator DisableCompletelyDelay()
    {
        yield return new WaitForSeconds(_disableDelay);

        this.gameObject.SetActive(false);
    }
}
