using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{    
    [SerializeField] private float _disableDelay;
    [SerializeField] private bool _opensOnce = false;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
    }

    protected override void Interact(Interactable interactable)
    {
        Debug.Log("DoorInteractable - Interact - STARTED");
        if (interactable != this)
            return;

        _anim.enabled = true;

        if(_opensOnce)
        {
            Debug.Log("DoorInteractable - Interact - Triggered");

            // TODO play sfx
            // ...

            // reset interaction prompt
            EventAggregator.GetEvent<CannotInteractEvent>().Publish();

            // change layer 
            this.gameObject.layer = 0; // TODO maybe change to different layer

            _canInteract = false;
        }
    }    
}
