using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string _id;
    public string GetId() => _id;

    [SerializeField]
    protected string _promptMessage;
    [SerializeField]
    protected float _interactionCooldown;
    [SerializeField]
    protected bool _canInteract = true;
    [SerializeField]
    protected bool _canInteractSecondTime = false;

    protected void OnEnable()
    {
        _id = Guid.NewGuid().ToString();
        EventAggregator.GetEvent<InteractEvent>().Subscribe(Interact);
    }
    protected void OnDisable()
    {
        EventAggregator.GetEvent<InteractEvent>().UnSubscribe(Interact);
    }
    public bool GetCanInteract() => _canInteract;

    public string GetPromptMessage() => _promptMessage;

    protected virtual void Interact(string interactableId)
    {
        Debug.Log("Interaction triggered. Id: " + interactableId);

        if (interactableId != this._id)
        {
            Debug.Log($"Interactable {interactableId} is not this one {this._id}");
            return;
        }
        else
            Debug.Log($"interactable {interactableId} is {this.gameObject.name}: {interactableId == this._id}");

        if (_canInteractSecondTime)
        {
            StartCoroutine(InteractCooldown());
        }
    }

    private IEnumerator InteractCooldown()
    {
        _canInteract = false;

        yield return new WaitForSeconds(_interactionCooldown);

        _canInteract = true;
    }

}
