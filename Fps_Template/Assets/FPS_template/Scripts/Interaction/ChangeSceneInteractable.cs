using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneChanger))]
public class ChangeSceneInteractable : Interactable
{
    [SerializeField] private float _delay;
    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
    }

    protected override void Interact(Interactable interactable)
    {
        Debug.Log("ChangeSceneInteractable - Interact - STARTED");
        if (interactable != this)
            return;

        Debug.Log("DoorInteractable - Interact - Triggered");

        // TODO play sfx
        // ...

        _sceneChanger.LoadNewScene();

        // reset interaction prompt
        EventAggregator.GetEvent<CannotInteractEvent>().Publish();
               
        _canInteract = false;
    }
}
