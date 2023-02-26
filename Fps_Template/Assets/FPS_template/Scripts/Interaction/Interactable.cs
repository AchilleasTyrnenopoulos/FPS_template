using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string _promptMessage;
    [SerializeField]
    protected float _interactionCooldown;
    [SerializeField]
    protected bool _canInteract = true;

    private void OnEnable()
    {
        EventAggregator.GetEvent<InteractEvent>().Subscribe(Interact);
    }
    private void OnDisable()
    {
        EventAggregator.GetEvent<InteractEvent>().Unsubscribe(Interact);
    }
    public bool GetCanInteract() => _canInteract;

    public string GetPromptMessage() => _promptMessage;

    protected virtual void Interact(Interactable interactable)
    {
        if (interactable != this) return;

        Debug.Log($"interacted with {this.gameObject.name}");
        StartCoroutine(InteractCooldown());
    }

    private IEnumerator InteractCooldown()
    {
        _canInteract = false;

        yield return new WaitForSeconds(_interactionCooldown);

        _canInteract = true;
    }

}
